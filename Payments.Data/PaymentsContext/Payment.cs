using System;
using System.Collections.Generic;

#nullable disable

namespace Payments.Data.PaymentsContext
{
    public partial class Payment
    {
        public int Id { get; set; }
        public decimal PaymentFee { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public decimal Ufe { get; set; }
        public int ClientCardId { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual ClientCard ClientCard { get; set; }
    }
}
