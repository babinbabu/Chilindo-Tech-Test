using ChilindoTechTest.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChilindoTechTest.Core.Model
{
    public class TransactionModel
    {
        public int? AccountNumber { get; set; }
        public Decimal? Amount { get; set; }
        public string Currency { get; set; }
        public TransactionModel()
        {

        }


        public void Validate()
        {
            if (AccountNumber == null)
            {
                throw new WebApiException(new WebApiError(WebApiErrorCode.InvalidArguments, "Account Number cannot be null or empty"));
            }

            if (Amount == null)
            {
                throw new WebApiException(new WebApiError(WebApiErrorCode.InvalidArguments, "Amount cannot be null or empty"));
            }

            if (string.IsNullOrEmpty(Currency))
            {
                throw new WebApiException(new WebApiError(WebApiErrorCode.InvalidArguments, "Currency cannot be null or empty"));
            }
        }
    }



}