using System;
using System.Collections.Generic;

#nullable disable

namespace Payments.Data.PaymentsContext
{
    public partial class Credit
    {
        public int Id { get; set; }
        public int ClientCardId { get; set; }
        public decimal CreditAmount { get; set; }
    }
}
