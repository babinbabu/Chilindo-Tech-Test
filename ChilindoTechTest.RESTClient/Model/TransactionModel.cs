using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChilindoTechTest.RESTClient.Model
{
    public class TransactionModel
    {
        public int? AccountNumber { get; set; }
        public Decimal? Amount { get; set; }
        public string Currency { get; set; }
    }
}
