using ChilindoTechTest.Core.Core.Repositories;
using ChilindoTechTest.Core.Model;
using ChilindoTechTest.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChilindoTechTest.Common.Helpers;

namespace ChilindoTechTest.Core.Persistence.Repositories
{
    public class AccountRepository : IAccount
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Balance
        /// <summary>
        /// Get The Balance
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        public BalanceViewmodel Balance(int? accountNo)
        {
            
            if (accountNo == null)
            {
                throw new WebApiException(new WebApiError(WebApiErrorCode.BadRequest));
            }
            using (Entity.ChilindoTechTestEntities db = new Entity.ChilindoTechTestEntities())
            {
                try
                {
                    log.Info(accountNo);
                    var entityAccountDetails = db.AccountDetails.FirstOrDefault(x => x.AccountNumber == accountNo);
                    if (entityAccountDetails != null)
                    {

                        return new BalanceViewmodel(entityAccountDetails, "Your Balance transaction successful");
                    }
                    else
                    {
                        throw new WebApiException(new WebApiError(WebApiErrorCode.NotFound, "Account number is not found", accountNo));
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    throw;

                }
            }
        }
        #endregion

        #region Deposit
        /// <summary>
        /// Perform Deposit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BalanceViewmodel Deposit(TransactionModel model)
        {

            model.Validate();

            using (Entity.ChilindoTechTestEntities db = new Entity.ChilindoTechTestEntities())
            {
                if (!Enum.IsDefined(typeof(Common.Enums.ChilindoCurrencyType), model.Currency))
                {
                    throw new WebApiException(new WebApiError(WebApiErrorCode.NotFound, "Currency type Not Found", model.AccountNumber));
                }
                decimal convertedAmount = 0;
                var entityAccountDetails = db.AccountDetails.FirstOrDefault(x => x.AccountNumber == model.AccountNumber);

                if (entityAccountDetails != null)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var entityAccountTransactions = db.AccountTransactions.Create();
                            entityAccountTransactions.AccountNumber = model.AccountNumber.Value;
                            entityAccountTransactions.Amount = model.Amount.Value;
                            entityAccountTransactions.CurrencyType = (Common.Enums.ChilindoCurrencyType)Enum.Parse(typeof(Common.Enums.ChilindoCurrencyType), model.Currency);
                            entityAccountTransactions.TransactionType = Common.Enums.ChilindoTransactionType.Credit;
                            entityAccountTransactions.TransactionDate = DateTime.UtcNow;
                            db.AccountTransactions.Add(entityAccountTransactions);
                            convertedAmount = CurrencyExchange.CurrencyConversion(model.Amount.Value, model.Currency, Common.Enums.ChilindoCurrencyType.USD.ToString());
                            entityAccountDetails.Balance = entityAccountDetails.Balance + Math.Max(0, convertedAmount);
                            db.SaveChanges();
                            transaction.Commit();
                            return new BalanceViewmodel(entityAccountDetails, "Your deposit transaction successful");
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    throw new WebApiException(new WebApiError(WebApiErrorCode.NotFound, "Account Number Not Found", model.AccountNumber));
                }
            }
        }
        #endregion

        #region Withdrawal
        /// <summary>
        /// Perform Withdrawal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BalanceViewmodel Withdraw(TransactionModel model)
        {

            model.Validate();

            if (!Enum.IsDefined(typeof(Common.Enums.ChilindoCurrencyType), model.Currency))
            {
                throw new WebApiException(new WebApiError(WebApiErrorCode.NotFound, "Currency type Not Found", model.AccountNumber));
            }
            using (Entity.ChilindoTechTestEntities db = new Entity.ChilindoTechTestEntities())
            {
                decimal convertedAmount = 0;
                convertedAmount = CurrencyExchange.CurrencyConversion(model.Amount.Value, model.Currency, Common.Enums.ChilindoCurrencyType.USD.ToString());
                convertedAmount = Math.Max(0, convertedAmount);
                var entityAccountDetails = db.AccountDetails.FirstOrDefault(x => x.AccountNumber == model.AccountNumber);

                if (entityAccountDetails != null)
                {
                    if (convertedAmount > entityAccountDetails.Balance)
                    {
                        throw new WebApiException(new WebApiError(WebApiErrorCode.InvalidAction, "Sorry, Insufficient Fund to Withdraw", entityAccountDetails.AccountNumber));
                    }
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var entityAccountTransactions = db.AccountTransactions.Create();
                            entityAccountTransactions.AccountNumber = model.AccountNumber.Value;
                            entityAccountTransactions.Amount = model.Amount.Value;
                            entityAccountTransactions.CurrencyType = (Common.Enums.ChilindoCurrencyType)Enum.Parse(typeof(Common.Enums.ChilindoCurrencyType), model.Currency);
                            entityAccountTransactions.TransactionType = Common.Enums.ChilindoTransactionType.Debit;
                            entityAccountTransactions.TransactionDate = DateTime.UtcNow;
                            db.AccountTransactions.Add(entityAccountTransactions);
                            entityAccountDetails.Balance = entityAccountDetails.Balance - convertedAmount;
                            db.SaveChanges();
                            transaction.Commit();
                            return new BalanceViewmodel(entityAccountDetails, "Your withdraw transaction successful");
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    throw new WebApiException(new WebApiError(WebApiErrorCode.NotFound, "Account Number Not Found", model.AccountNumber));
                }
            }
        }
        #endregion


    }
}