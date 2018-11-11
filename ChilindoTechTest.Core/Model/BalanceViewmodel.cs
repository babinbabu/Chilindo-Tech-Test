using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChilindoTechTest.Core.Model
{
   public class BalanceViewmodel
    {
        public bool Successful { get; set; }
        public int AccountNumber { get; set; }
        public Decimal? Balance { get; set; }
        public string Currency { get; set; }
        public string Message { get; set; }

        public BalanceViewmodel()
        {

        }

        
        public BalanceViewmodel(Entity.AccountDetail entityAccountDetail, string message = null, bool currencyIndicator = false)
        {
            Successful =  true;
            AccountNumber = entityAccountDetail.AccountNumber;
            Balance = entityAccountDetail.Balance;
            Currency = entityAccountDetail.CurrencyType.ToString();
            Message = message;

        }

        public BalanceViewmodel(BalanceViewmodel model)
        {
            Successful = false;
            AccountNumber = model.AccountNumber;
            Balance = null;
            Currency = null;
            Message = model.Message;

        }
    }
}