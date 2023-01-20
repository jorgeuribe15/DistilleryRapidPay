using System;
using System.Collections.Generic;

#nullable disable

namespace Payments.Data.PaymentsContext
{
    public partial class ClientCard
    {
        public ClientCard()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int CardId { get; set; }
        public int ClientId { get; set; }
        public bool IsActive { get; set; }

        public virtual Card Card { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
