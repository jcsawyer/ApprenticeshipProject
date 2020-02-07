using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FirstCatering.Data;
using FirstCatering.Lib.IoC;
using FirstCatering.Lib.AspNetCore.Extensions;
using FirstCatering.Lib.Logging;
using System.Threading.Tasks;
using FirstCatering.Models.Employee;
using FirstCatering.Domain;
using Microsoft.EntityFrameworkCore;
using FirstCatering.Services.Transaction;
using FirstCatering.Models.Transaction;

namespace FirstCatering.Services.Tests
{
    [TestClass]
    public class TransactionServiceTests
    {
        private FirstCateringDbContext Db { get; }
        private ITransactionService Service { get; }

        public TransactionServiceTests()
        {
            var services = new ServiceCollection();
            services.AddDbContextMemory<FirstCateringDbContext>();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<ITransactionService, TransactionService>();

            Db = services.BuildServiceProvider().GetService<FirstCateringDbContext>();
            Service = services.BuildServiceProvider().GetService<ITransactionService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SpendThrowsArgumentNullExceptionWhenRequestIsNull()
        {
            await Service.Spend(null, 0);
        }

        [TestMethod]
        public async Task SpendReturnsErrorWhenAmountIsZero()
        {
            var request = new SpendRequestModel { Amount = 0M };
            var response = await Service.Spend(request, 1L);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.Message, "Cannot spend zero or negative amount");
        }

        [TestMethod]
        public async Task SpendReturnsErrorWhenAmountIsNegative()
        {
            var request = new SpendRequestModel { Amount = -10M };
            var response = await Service.Spend(request, 1L);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.Message, "Cannot spend zero or negative amount");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SpendThrowsInvalidOperationExceptionWhenEmployeeNotFound()
        {
            var request = new SpendRequestModel { Amount = 10M };
            var response = await Service.Spend(request, 10L);
        }

        [TestMethod]
        public async Task SpendReturnsErrorWhenNotEnoughBalance()
        {
            var employee = new EmployeeEntity(default, "asdfghjklqwertyu", "Test", "test@nomail.com", "07482918302", 1L, "0000", 5M);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new SpendRequestModel { Amount = 10M };
            var response = await Service.Spend(request, employee.Id);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.Message, "Not enough balance");
        }

        [TestMethod]
        public async Task SpendCreatesNewTransactionEntryOnSuccess()
        {
            var employee = new EmployeeEntity(default, "as0394jklqwertyu", "Test", "test@nomail.com", "07482918302", 1L, "0000", 10M);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new SpendRequestModel { Amount = 5M };
            var response = await Service.Spend(request, employee.Id);

            Assert.IsTrue(response.IsSuccess);

            var transactionCount = await Db.Set<TransactionEntity>().CountAsync(x => x.EmployeeId == employee.Id && x.Amount == -request.Amount);
            Assert.AreEqual(1, transactionCount);
        }
        [TestMethod]
        public async Task SpendReturnsNewEmployeeBalanceOnSuccess()
        {
            decimal startingBalance = 10M;
            var employee = new EmployeeEntity(default, "as039928lq550tyu", "Test", "test@nomail.com", "07482918302", 1L, "0000", startingBalance);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new SpendRequestModel { Amount = 5M };
            var response = await Service.Spend(request, employee.Id);

            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(response.Data, startingBalance - request.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TopUpThrowsArgumentNullExceptionWhenRequestIsNull()
        {
            await Service.TopUp(null, 0);
        }

        [TestMethod]
        public async Task TopUpReturnsErrorWhenAmountIsZero()
        {
            var request = new TopUpRequestModel { Amount = 0M };
            var response = await Service.TopUp(request, 1L);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.Message, "Cannot top up zero or negative amount");
        }

        [TestMethod]
        public async Task TopUpReturnsErrorWhenAmountIsNegative()
        {
            var request = new TopUpRequestModel { Amount = -10M };
            var response = await Service.TopUp(request, 1L);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.Message, "Cannot top up zero or negative amount");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task TopUpThrowsInvalidOperationExceptionWhenEmployeeNotFound()
        {
            var request = new TopUpRequestModel { Amount = 10M };
            var response = await Service.TopUp(request, 15L);
        }

        [TestMethod]
        public async Task TopUpCreatesNewTransactionEntryOnSuccess()
        {
            var employee = new EmployeeEntity(default, "as0028jklqwertyu", "Test", "test@nomail.com", "07482918302", 1L, "0000", 10M);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new TopUpRequestModel { Amount = 5M };
            var response = await Service.TopUp(request, employee.Id);

            Assert.IsTrue(response.IsSuccess);

            var transactionCount = await Db.Set<TransactionEntity>().CountAsync(x => x.EmployeeId == employee.Id && x.Amount == request.Amount);
            Assert.AreEqual(1, transactionCount);
        }

        [TestMethod]
        public async Task TopUpReturnsNewEmployeeBalanceOnSuccess()
        {
            decimal startingBalance = 10M;
            var employee = new EmployeeEntity(default, "as031328lq550tyu", "Test", "test@nomail.com", "07482918302", 1L, "0000", startingBalance);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new TopUpRequestModel { Amount = 5M };
            var response = await Service.TopUp(request, employee.Id);

            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(response.Data, startingBalance + request.Amount);
        }
    }
}
