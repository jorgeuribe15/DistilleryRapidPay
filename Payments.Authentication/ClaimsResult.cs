using Microsoft.AspNetCore.Authentication;
using System;
using System.Security.Claims;

namespace Payments.Authentication
{
    public class ClaimsResult
    {
        public static Claim[] claims { get; set; }
        public static ClaimsIdentity identity { get; set; }
        public static ClaimsPrincipal principal { get; set; }
        public static AuthenticationTicket authntiationAccess { get; set; }

        public static string username { get; set; }
        public static string password { get; set; }

        public static byte[] bytes { get; set; }
        public static string[] credentials { get; set; }
    }
}
