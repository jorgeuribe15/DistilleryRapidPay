using Payments.Data.PaymentsContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Data.BL
{
    public interface IUsersBL
    {
        Task<User> GetUser(string username, string password);
    }
}
