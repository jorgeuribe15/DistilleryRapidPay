using System;
using System.Collections.Generic;

#nullable disable

namespace Payments.Data.PaymentsContext
{
    public partial class User
    {
        public int ClientId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual Client Client { get; set; }
    }
}
