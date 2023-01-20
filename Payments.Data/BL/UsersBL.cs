using Microsoft.EntityFrameworkCore;
using Payments.Data.PaymentsContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Data.BL
{
    public class UsersBL : IUsersBL
    {
        protected readonly RapidPayDbContext _context;

        public UsersBL(RapidPayDbContext rapidPayDbContext)
        {
            _context = rapidPayDbContext;
        }

        public async Task<User> GetUser(string username, string password)
        {
            try
            {
                User user = await _context.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
                return user;                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
