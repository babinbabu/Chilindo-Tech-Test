using ChilindoTechTest.Core.Core.Repositories;
using ChilindoTechTest.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChilindoTechTest.Api.Controllers.Api
{
    public class AccountController : ApiController
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            this._account = account;
        }

        [HttpGet]
        public BalanceViewmodel Balance(int accountNo)
        {
            return WebApiWrapper.Call<BalanceViewmodel>(e => _account.Balance(accountNo));
        }

        [HttpPost]
        public BalanceViewmodel Deposit([FromBody]TransactionModel model)
        {
            return WebApiWrapper.Call<BalanceViewmodel>(e => _account.Deposit(model));
        }

        [HttpPost]
        public BalanceViewmodel Withdraw([FromBody]TransactionModel model)
        {
            return WebApiWrapper.Call<BalanceViewmodel>(e => _account.Withdraw(model));
        }
    }
}