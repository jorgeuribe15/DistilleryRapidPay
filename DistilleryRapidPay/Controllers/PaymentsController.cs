using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payments.Data.BL;
using Payments.Data.PaymentsContext;
using Payments.Data.PaymentsDtos;
using Payments.Service;
using System.Threading.Tasks;

namespace Payments.Cards.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private IPaymentsBL _iPaymentsBL;
        private decimal _balance;

        public PaymentsController(IPaymentsBL paymentsBL)
        {
            _iPaymentsBL = paymentsBL;
        }

        [HttpPost("setpayment")]
        public async Task<ActionResult<Payment>> PostPayment(PaymentDTO paymentdto)
        {
            Payment payment = await _iPaymentsBL.SetPayment(paymentdto);
            if (payment != null)
            {
                return Ok(new { UFE = payment.Ufe, Transactionmunt = payment.Amount, TotalPayment = payment.PaymentFee });
            }

            return BadRequest("Error in process or Transaction is paid");
        }


        [HttpGet]
        [Route("{accountNumber}")]
        public async Task<ActionResult<decimal>> GetBalance(string accountNumber)
        {
             _balance = await _iPaymentsBL.GetBalanceByAccountNumber(accountNumber);

            return Ok(new { AccountBalance = _balance});
        }
    }
}
