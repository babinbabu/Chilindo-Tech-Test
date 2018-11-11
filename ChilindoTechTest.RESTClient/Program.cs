using ChilindoTechTest.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using ChilindoTechTest.RESTClient.Model;
using ChilindoTechTest.RESTClient.Helper;
using Newtonsoft.Json.Linq;


namespace ChilindoTechTest.RESTClient
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(10);

            var balance = new System.Threading.Timer((e) =>
            {
                Balance().Wait();
            }, null, startTimeSpan, periodTimeSpan);

            var withdraw = new System.Threading.Timer((e) =>
            {
                Withdraw().Wait();
            }, null, startTimeSpan, periodTimeSpan);

            var deposit = new System.Threading.Timer((e) =>
            {
                Deposit().Wait();
            }, null, startTimeSpan, periodTimeSpan);

            Console.ReadLine();
        }

        public static async Task Balance()
        {
            
            try
            {
               var accountNumber= Helpers.GetRandomAccountValue();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Helpers.ApiBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("/api/Account/Balance?accountNo=" + accountNumber);
                    BalanceViewmodel result = await response.Content.ReadAsAsync<BalanceViewmodel>();
                    string finalResult = string.Format("Successful : {0}\n AccountNumber : {1}\n Balance : {2}\n Currency : {3}\n Message : {4}", result.Successful, result.AccountNumber, result.Balance, result.Currency, result.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*** Balance is executed at {0} ***", DateTime.Now);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(finalResult);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(finalResult);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task Deposit()
        {
           
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Helpers.ApiBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    TransactionModel deposit = new TransactionModel();
                    deposit.AccountNumber = Helpers.GetRandomAccountValue();
                    deposit.Amount = (decimal)Helpers.RandomNumberBetween(0, 200000);
                    deposit.Currency = Helpers.GetRandomCurrencyType();
                    HttpResponseMessage response = await client.PostAsJsonAsync("/api/Account/Deposit", deposit);
                    BalanceViewmodel result = await response.Content.ReadAsAsync<BalanceViewmodel>();
                    string finalResult = string.Format("Successful : {0}\n AccountNumber : {1}\n Balance : {2}\n Currency : {3}\n Message : {4}", result.Successful, result.AccountNumber, result.Balance, result.Currency, result.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*** Deposit is executed at {0} ***", DateTime.Now);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(finalResult);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(finalResult);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static async Task Withdraw()
        {
            
            try
            {
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Helpers.ApiBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    TransactionModel withdraw = new TransactionModel();
                    withdraw.AccountNumber = Helpers.GetRandomAccountValue();
                    withdraw.Amount = (decimal)Helpers.RandomNumberBetween(0, 200000);
                    withdraw.Currency = Helpers.GetRandomCurrencyType();
                    HttpResponseMessage response = await client.PostAsJsonAsync("/api/Account/Withdraw", withdraw);
                    BalanceViewmodel result = await response.Content.ReadAsAsync<BalanceViewmodel>();
                    string finalResult = string.Format("Successful : {0}\n AccountNumber : {1}\n Balance : {2}\n Currency : {3}\n Message : {4}", result.Successful, result.AccountNumber, result.Balance, result.Currency, result.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*** Withdraw is executed at {0} ***", DateTime.Now);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(finalResult);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(finalResult);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
