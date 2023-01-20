using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payments.Data.BL;
using Payments.Data.PaymentsContext;
using Payments.Data.PaymentsDtos;
using System;
using System.Threading.Tasks;

namespace Payments.Cards.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardManagementController : ControllerBase
    {
        private ICardBL _iCardBL;

        public CardManagementController(ICardBL cardsBL)
        {
            _iCardBL = cardsBL;
        }

        [HttpPost("createcard")]
        public async Task<IActionResult> PostCard(NewCardDTO newcardDTO)
        {
            NewClientCard newcard = await _iCardBL.CreateCard(newcardDTO);

            Card createdCard = await _iCardBL.CreateNewClientCard(newcard);

            if (createdCard != null)
            {
                return Ok(new { AccNumber = createdCard.AccountNumber, ExpirationDate = createdCard.ExpirationDate.ToShortDateString() });
            }

            return BadRequest(); ;
        }

        private IActionResult OK(object p)
        {
            throw new NotImplementedException();
        }
    }
}
