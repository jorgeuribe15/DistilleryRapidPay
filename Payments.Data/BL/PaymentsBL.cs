using Microsoft.EntityFrameworkCore;
using Payments.Data.PaymentsContext;
using Payments.Data.PaymentsDtos;
using Payments.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Data.BL
{
    public class PaymentsBL : IPaymentsBL
    {
        protected readonly RapidPayDbContext _context;
        private IUFEs _uFEs;

        public PaymentsBL(RapidPayDbContext rapidPayDbContext, IUFEs uFEs)
        {
            _context = rapidPayDbContext;
            _uFEs = uFEs;            
        }

        public async Task<Payment> SetPayment(PaymentDTO paymentdto)
        {   
            try
            {
                int clientcardId = _context.ClientCards.Where(cc => cc.ClientId == paymentdto.ClientId && cc.Card.AccountNumber == paymentdto.CardNumber).Select(s => s.Id).FirstOrDefault();

                // GET UEF
                int fee = _uFEs.GetUfeValue();
                // set payment
                Payment  newpayment = new Payment
                {
                    PaymentFee = fee == 0 ? paymentdto.Amount : paymentdto.Amount * (decimal)(1 * fee),
                    TransactionId = 0,
                    Amount = paymentdto.Amount,
                    Ufe = fee,
                    PaymentDate = DateTime.Now,
                    ClientCardId = clientcardId
                };

                await _context.Payments.AddAsync(newpayment);
                await _context.SaveChangesAsync();

                return newpayment;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public async Task<decimal> GetBalanceByAccountNumber(string accountNumber)
        {
            int cardId = await _context.Cards.Where(c => c.AccountNumber == accountNumber).Select(s => s.Id).FirstOrDefaultAsync();

            if (cardId == 0)
            {
                return 0;
            }
            else
            {
                decimal creditAmmount = _context.Credits.Where(c => c.ClientCardId == cardId).Select(s => s.CreditAmount).FirstOrDefault();
                var totalPayments = (from t in _context.Payments                                     
                                     where t.ClientCardId == cardId
                                     select new
                                     {
                                         t.PaymentFee
                                     }).Sum(s => s.PaymentFee);

                decimal balance = creditAmmount - totalPayments;

                return balance;
            }

        }

    }
}
