using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payments.Data.BL;
using Payments.Data.PaymentsContext;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static Payments.Authentication.ClaimsResult;

namespace Payments.Authentication
{
        public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
        {
            private readonly IUsersBL _usersBL;
            public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUsersBL usersBL) : base(options, logger, encoder, clock)
            {
                _usersBL = usersBL;
            }

            protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    return AuthenticateResult.Fail("Authorization header was not found");
                }

                try
                {
                    var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                    bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                    credentials = Encoding.UTF8.GetString(bytes).Split(":");

                    username = credentials[0];
                    password = credentials[1];

                    User user = await _usersBL.GetUser(username, password);
                    if (user == null)
                    {
                        return AuthenticateResult.Fail("Invalid username or password");
                    }
                    else
                    {
                        claims = new[] { new Claim(ClaimTypes.Name, user.Username) };
                        identity = new ClaimsIdentity(claims, Scheme.Name);
                        principal = new ClaimsPrincipal(identity);
                        authntiationAccess = new AuthenticationTicket(principal, Scheme.Name);

                        return AuthenticateResult.Success(ClaimsResult.authntiationAccess);
                    }
                }
                catch (Exception ex)
                {
                    return AuthenticateResult.Fail($"Authentication process throw exception: {ex.Message}");
                }
            }
        }

    
}
