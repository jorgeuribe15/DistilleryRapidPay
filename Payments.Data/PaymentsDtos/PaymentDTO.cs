using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Data.PaymentsDtos
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }        
        public decimal Amount { get; set; }        
        public DateTime PaymentDate { get; set; }
        public string CardNumber { get; set; }
    }
}
