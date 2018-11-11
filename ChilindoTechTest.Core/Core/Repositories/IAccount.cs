using ChilindoTechTest.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChilindoTechTest.Core.Core.Repositories
{
    public interface IAccount
    {
        #region Account
        BalanceViewmodel Balance(int? accountNo);
        BalanceViewmodel Deposit(TransactionModel model);
        BalanceViewmodel Withdraw(TransactionModel model);
        #endregion
    }

}
