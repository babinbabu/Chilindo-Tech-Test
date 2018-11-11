using System;
using ChilindoTechTest.Api.Controllers.Api;
using ChilindoTechTest.Core.Core.Repositories;
using ChilindoTechTest.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using System.Web.Http;

namespace ChilindoTechTest.UnitTest
{
    [TestClass]
    public class ChilindoUnitTest
    {

        [TestMethod]
        public void BalanceCheck()
        {
            // Arrange
            var mockRepository = new Mock<IAccount>();
            mockRepository.Setup(x => x.Balance(1000))
                .Returns(new BalanceViewmodel { Successful = true });

            var controller = new AccountController(mockRepository.Object);

            // Act
            BalanceViewmodel actionResult = controller.Balance(1000);


            // Assert
            Assert.IsNotNull(actionResult);

            Assert.AreEqual(true, actionResult.Successful);


        }

        [TestMethod]
        public void BalanceCheckAccountNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IAccount>();
            mockRepository.Setup(x => x.Balance(10000))
                .Returns(new BalanceViewmodel { Successful = false });

            var controller = new AccountController(mockRepository.Object);

            // Act
            BalanceViewmodel actionResult = controller.Balance(10000);


            // Assert
            Assert.IsNotNull(actionResult);

            Assert.AreEqual(false, actionResult.Successful);



        }

        [TestMethod]
        public void WithdrawSuccessful()
        {
            TransactionModel model = new TransactionModel();
            model.AccountNumber = 1000;
            model.Amount = 10;
            model.Currency = "THB";
            // Arrange
            var mockRepository = new Mock<IAccount>();
            mockRepository.Setup(x => x.Withdraw(model))
            .Returns(new BalanceViewmodel { Successful = true });

            var controller = new AccountController(mockRepository.Object);

            // Act
            BalanceViewmodel actionResult = controller.Withdraw(model);


            // Assert
            Assert.IsNotNull(actionResult);

            Assert.AreEqual(true, actionResult.Successful);



        }

        [TestMethod]
        public void WithdrawInsufficientFund()
        {
            TransactionModel model = new TransactionModel();
            model.AccountNumber = 1000;
            model.Amount = 1000000;
            model.Currency = "USD";
            // Arrange
            var mockRepository = new Mock<IAccount>();
            mockRepository.Setup(x => x.Withdraw(model))
            .Returns(new BalanceViewmodel { Successful = false });

            var controller = new AccountController(mockRepository.Object);

            // Act
            BalanceViewmodel actionResult = controller.Withdraw(model);


            // Assert
            Assert.IsNotNull(actionResult);

            Assert.AreEqual(false, actionResult.Successful);



        }
        [TestMethod]
        public void DepositSuccessful()
        {
            TransactionModel model = new TransactionModel();
            model.AccountNumber = 1000;
            model.Amount = 100;
            model.Currency = "INR";
            // Arrange
            var mockRepository = new Mock<IAccount>();
            mockRepository.Setup(x => x.Deposit(model))
                .Returns(new BalanceViewmodel { Successful = true });

            var controller = new AccountController(mockRepository.Object);

            // Act
            BalanceViewmodel actionResult = controller.Deposit(model);


            // Assert
            Assert.IsNotNull(actionResult);

            Assert.AreEqual(true, actionResult.Successful);



        }


    }
}
