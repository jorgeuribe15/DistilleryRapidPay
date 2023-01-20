using System;
using System.Collections.Generic;
using System.Linq;

namespace Payments.Service
{
    public class UFEs : IUFEs
    {

        string currentDate = string.Empty;
        private List<int> ufes = new List<int>();
        private int nextUfe = 0;
        int value = 0;
        Dictionary<string, int> feeDictionary = new Dictionary<string, int>();

        public UFEs()
        {
            ufes.Add(0);
            ufes.Add(1);
            ufes.Add(2);
        }
        public int GetUfeValue()
        {
            InsertFee();
            nextUfe = GetFee();

            value = ufes[nextUfe];

            return value;
        }

        private int GetFee()
        {
            
            int fee = 0;

            if(feeDictionary.ContainsKey(currentDate))
            {
                feeDictionary.TryGetValue(currentDate, out fee);
            }
            return fee;
        }

        private void InsertFee() // Day Valiation
        {
            var rUfes = new Random();
            
            nextUfe = rUfes.Next(0, 3);
            currentDate = DateTime.Now.ToShortDateString();
            

            if (!feeDictionary.ContainsKey(currentDate))
            {
                feeDictionary.Remove(currentDate);
            }

            if (feeDictionary.ContainsKey(currentDate))
            {
                return;
            }
            else if(!feeDictionary.ContainsKey(currentDate))
            {                
                feeDictionary.Add(currentDate, nextUfe);
            }

        }

    }
}
