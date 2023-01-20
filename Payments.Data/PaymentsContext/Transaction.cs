using System;
using System.Collections.Generic;

#nullable disable

namespace Payments.Data.PaymentsContext
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int ClientCardId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TraansactionPlace { get; set; }
    }
}
