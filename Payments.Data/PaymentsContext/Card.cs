using System;
using System.Collections.Generic;

#nullable disable

namespace Payments.Data.PaymentsContext
{
    public partial class Card
    {
        public Card()
        {
            ClientCards = new HashSet<ClientCard>();
        }

        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ClientCard> ClientCards { get; set; }
    }
}
