using Microsoft.EntityFrameworkCore;
using Payments.Data.PaymentsContext;
using Payments.Data.PaymentsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Data.BL
{
    public class CardBL : ICardBL
    {
        protected readonly RapidPayDbContext _context;
        private string newAccountNumber = string.Empty;
        const int maxCardLength = 14;
        int cont = 1;

        public CardBL(RapidPayDbContext rapidPayDbContext)
        {
            _context = rapidPayDbContext;
        }
   
        public async Task<NewClientCard> CreateCard(NewCardDTO newcardDTO)
        {
            try
            {
                CardDTO card = new CardDTO();
                
                card.AccountNumber = await CreateNewAccountNumber();

                Card newCard = new Card()
                {
                    AccountNumber = card.AccountNumber,
                    Type = "CREDIT",
                    ExpirationDate = DateTime.Now.AddYears(5),
                    IsActive = true
                };

                await _context.Cards.AddAsync(newCard);

                Client client = new Client
                {
                    Name = newcardDTO.ClientName,
                    Dob = newcardDTO.DOB,
                    IsActive = true
                };

                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();

                int cid = client.Id;
                int ncard = newCard.Id;

                NewClientCard newClientCard = new NewClientCard
                {
                    CarId = ncard,
                    ClientId = cid
                };

                return newClientCard;
            }
            catch (Exception ex)
            {
                // log ex
                return new NewClientCard();
            }
        }

        private void CreateClientCad(int clientId, int newCarId)
        {
            ClientCard clientCard = new ClientCard
            {
                CardId = newCarId,
                ClientId = clientId,
                IsActive = true
            };

            _context.ClientCards.Add(clientCard);
            _context.SaveChanges();
        }
       

        private async Task<string> CreateNewAccountNumber()
        {
            var cardNumber = new Random();
            int newCardId = cardNumber.Next(10, 99);

            newAccountNumber = newCardId.ToString().PadLeft(14, '0');

            if(string.IsNullOrEmpty(newAccountNumber))
            {
                newAccountNumber = newCardId.ToString().PadRight(14, '0');
            }

            if(await ValidateCard(newAccountNumber))
            {
                return newAccountNumber;
            }
            
            return string.Empty;
        }

        private async Task<bool>  ValidateCard(string cardNumber)
        {
            if (_context.Cards.Any(c => c.AccountNumber == cardNumber))
            {
                if (cont <= 5)
                {
                    await CreateNewAccountNumber();
                    cont++;
                }
                return false;
            }

            return true;
        }

        public async Task<Card> CreateNewClientCard(NewClientCard newcard)
        {
            ClientCard clientCard = new ClientCard
            {
                CardId = newcard.CarId,
                ClientId = newcard.ClientId,
                IsActive = true
            };

            await _context.ClientCards.AddAsync(clientCard);
            Card card = await _context.Cards.Where(c => c.Id == newcard.CarId).FirstOrDefaultAsync();

            await _context.SaveChangesAsync();

            

            return card;
        }
    }

}