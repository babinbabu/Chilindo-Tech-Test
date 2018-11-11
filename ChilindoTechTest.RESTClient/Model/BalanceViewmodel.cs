using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChilindoTechTest.RESTClient.Model
{
   public class BalanceViewmodel
    {
        public int AccountNumber { get; set; }
        public bool Successful { get; set; }
        public Decimal? Balance { get; set; }
        public string Currency { get; set; }
        public string Message { get; set; }
    }
}
