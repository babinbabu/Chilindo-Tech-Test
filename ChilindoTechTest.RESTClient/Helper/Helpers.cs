using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChilindoTechTest.RESTClient.Helper
{
    public class Helpers
    {
        private static readonly Random random = new Random();
        public static int GetRandomAccountValue()
        {
            Array values = Enum.GetValues(typeof(ChilindoTechTest.Common.Enums.DefaultAccount));
            Random random = new Random();
            ChilindoTechTest.Common.Enums.DefaultAccount randomDefaultAccount = (ChilindoTechTest.Common.Enums.DefaultAccount)values.GetValue(random.Next(values.Length));

            return (int)randomDefaultAccount;
        }
        public static string GetRandomCurrencyType()
        {
            Array values = Enum.GetValues(typeof(ChilindoTechTest.Common.Enums.ChilindoCurrencyType));
            Random random = new Random();
            ChilindoTechTest.Common.Enums.ChilindoCurrencyType randomDefaultAccount = (ChilindoTechTest.Common.Enums.ChilindoCurrencyType)values.GetValue(random.Next(values.Length));

            return randomDefaultAccount.ToString();
        }

        public static double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();

            return minValue + (next * (maxValue - minValue));
        }

        public static string ApiBaseUrl
        {
            get
            {
                return "http://localhost:57850";
            }
        }
    }
}
