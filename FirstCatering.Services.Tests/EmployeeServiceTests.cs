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
using FirstCatering.Lib.Security.Jwt;

namespace FirstCatering.Services.Tests
{
    [TestClass]
    public class EmployeeServiceTests
    {
        private FirstCateringDbContext Db { get; }
        private IEmployeeService Service { get; }
        private IJsonWebToken Jwt { get;  }

        public EmployeeServiceTests()
        {
            var services = new ServiceCollection();
            services.AddDbContextMemory<FirstCateringDbContext>();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ILogger, Logger>();
            services.AddHash(10000, 128);
            services.AddJsonWebToken(Guid.NewGuid().ToString(), TimeSpan.FromMinutes(5));
            services.AddAuthenticationJwtBearer();
            services.AddSingleton<IEmployeeService, EmployeeService>();

            Db = services.BuildServiceProvider().GetService<FirstCateringDbContext>();
            Service = services.BuildServiceProvider().GetService<IEmployeeService>();
            Jwt = services.BuildServiceProvider().GetService<IJsonWebToken>();
        }

        [TestMethod]
        public async Task IdentifyRequiresCompanyId()
        {
            var request = new IdentifyRequestModel { EmployeeId = "asdfghjklqwertyu" };
            var response = await Service.IdentifyAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("CompanyId is required", response.Message);
        }

        [TestMethod]
        public async Task IdentifyRequiresEmployeeId()
        {
            var request = new IdentifyRequestModel { CompanyId = 1L };
            var response = await Service.IdentifyAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("EmployeeId is required", response.Message);
        }

        [TestMethod]
        public async Task IdentifyReturnsErrorResultWhenCompanyDoesNotExist()
        {
            var request = new IdentifyRequestModel { CompanyId = 10L, EmployeeId = "lpq8r29fkf29dxsw" };
            var response = await Service.IdentifyAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Company not found", response.Message);
        }

        [TestMethod]
        public async Task IdentifyReturnsErrorResultWhenEmployeeDoesNotExist()
        {
            var request = new IdentifyRequestModel { CompanyId = 1L, EmployeeId = "lpq8r29fkf29dxsw" };
            var response = await Service.IdentifyAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Employee not found", response.Message);
        }

        [TestMethod]
        public async Task IdentifyReturnsEmployeeIdOnSuccess()
        {
            var employee = new EmployeeEntity(default, "asdfghjklqwertyu", "Test", "test@nomail.com", "07482918302", 1L, "0000", 0M);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new IdentifyRequestModel { CompanyId = 1L, EmployeeId = "asdfghjklqwertyu" };
            var response = await Service.IdentifyAsync(request);

            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(employee.Id, response.Data);
        }

        [TestMethod]
        public async Task RegisterRequiresEmployeeId()
        {
            var request = new RegisterRequestModel { Name = "Test", Email = "test@nomail.com", MobileNumber = "07482918302", CompanyId = 1L, PIN = "0000" };
            var response = await Service.RegisterAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("EmployeeId is required", response.Message);
        }

        [TestMethod]
        public async Task RegisterRequiresName()
        {
            var request = new RegisterRequestModel { EmployeeId = "asdfghjklqwertyu", Email = "test@nomail.com", MobileNumber = "07482918302", CompanyId = 1L, PIN = "0000" };
            var response = await Service.RegisterAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Name is required", response.Message);
        }

        [TestMethod]
        public async Task RegisterRequiresEmail()
        {
            var request = new RegisterRequestModel { EmployeeId = "asdfghjklqwertyu", Name = "Test", MobileNumber = "07482918302", CompanyId = 1L, PIN = "0000" };
            var response = await Service.RegisterAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Email is required", response.Message);
        }

        [TestMethod]
        public async Task RegisterRequiresMobileNumber()
        {
            var request = new RegisterRequestModel { EmployeeId = "asdfghjklqwertyu", Name = "Test", Email = "test@nomail.com", CompanyId = 1L, PIN = "0000" };
            var response = await Service.RegisterAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Mobile is required", response.Message);
        }

        [TestMethod]
        public async Task RegisterRequiresCompanyId()
        {
            var request = new RegisterRequestModel { EmployeeId = "asdfghjklqwertyu", Name = "Test", Email = "test@nomail.com", MobileNumber = "07482918302", PIN = "0000" };
            var response = await Service.RegisterAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("CompanyId is required", response.Message);
        }

        [TestMethod]
        public async Task RegisterRequiresPIN()
        {
            var request = new RegisterRequestModel { EmployeeId = "asdfghjklqwertyu", Name = "Test", Email = "test@nomail.com", MobileNumber = "07482918302", CompanyId = 1L };
            var response = await Service.RegisterAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("PIN is required", response.Message);
        }

        [TestMethod]
        public async Task RegisterReturnsErrorWhenCompanyNotFound()
        {
            var request = new RegisterRequestModel { EmployeeId = "asdfghjklqwertyu", Name = "Test", Email = "test@nomail.com", MobileNumber = "07482918302", CompanyId = 10L, PIN = "0000" };
            var response = await Service.RegisterAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Company not found", response.Message);
        }

        [TestMethod]
        public async Task RegisterReturnsErrorWhenEmployeeAlreadyExists()
        {
            var employee = new EmployeeEntity(default, "asdfghjklqwertyu", "Test", "test@nomail.com", "07482918302", 1L, "0000", 0M);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new RegisterRequestModel { EmployeeId = "asdfghjklqwertyu", Name = "Test", Email = "test@nomail.com", MobileNumber = "07482918302", CompanyId = 1L, PIN = "0000" };
            var response = await Service.RegisterAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Employee already registered", response.Message);
        }

        [TestMethod]
        public async Task RegisterHashesPINOnSuccess()
        {
            var employee = new EmployeeEntity(default, "asdfghjklqwertyu", "Test", "test@nomail.com", "07482918302", 1L, "0000", 0M);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            string pin = "0000";

            var request = new RegisterRequestModel { EmployeeId = "asdfghjklq43rtyu", Name = "Test", Email = "test@nomail.com", MobileNumber = "07482918302", CompanyId = 1L, PIN = pin };
            var response = await Service.RegisterAsync(request);

            Assert.AreNotEqual(pin, request.PIN);
        }

        [TestMethod]
        public async Task RegisterReturnsEmployeeIdOnSuccess()
        {
            var request = new RegisterRequestModel { EmployeeId = "asdfghjklq093tyu", Name = "Test", Email = "test@nomail.com", MobileNumber = "07482918302", CompanyId = 1L, PIN = "0000" };
            var response = await Service.RegisterAsync(request);

            Assert.IsTrue(response.IsSuccess);

            var result = await Db.Set<EmployeeEntity>().FindAsync((long)response.Data);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task LoginRequiresId()
        {
            var request = new LoginRequestModel { PIN = "0000", KioskId = "KIOSK01" };
            var response = await Service.LoginAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Id is required", response.Message);
        }

        [TestMethod]
        public async Task LoginRequiresPIN()
        {
            var request = new LoginRequestModel { Id = 1, KioskId = "KIOSK01" };
            var response = await Service.LoginAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("PIN is required", response.Message);
        }

        [TestMethod]
        public async Task LoginRequiresKioskId()
        {
            var request = new LoginRequestModel { Id = 1, PIN = "0000" };
            var response = await Service.LoginAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("KioskId is required", response.Message);
        }

        [TestMethod]
        public async Task LoginEmployeeNotFoundReturnsError()
        {
            var request = new LoginRequestModel { Id = 10L, PIN = "0000", KioskId = "KIOSK01" };
            var response = await Service.LoginAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Invalid login", response.Message);
        }

        [TestMethod]
        public async Task LoginWrongEmployeePINReturnsError()
        {
            var employee = new EmployeeEntity(default, "asdfgh8401wertyu", "Test", "test@nomail.com", "07482918302", 1L, "0000", 0M);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new LoginRequestModel { Id = employee.Id, PIN = "0001", KioskId = "KIOSK01" };
            var response = await Service.LoginAsync(request);

            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Invalid login", response.Message);
        }

        [TestMethod]
        public async Task LoginCreatesNewLoginEntryOnSuccess()
        {
            var employee = new EmployeeEntity(default, "a830498401wertyu", "Test", "test@nomail.com", "07482918302", 1L, "7bp/2HPesUX4boVouVA/6kNjy0HYc25ueL79bTtKPLiFQB7HzAVdQPZ3zRn+emvYlUiUHzVc8OUOSuseqt/EN6pjU6mzClxkHSY49P5fe5Yq/WCEzYplHjy5Fbcz/eIVqlqUEhFCrgJuMAhO9iD3fq0NewQxtkr1pg603gYDrsc=", 0M);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new LoginRequestModel { Id = employee.Id, PIN = "0000", KioskId = "KIOSK01" };
            var response = await Service.LoginAsync(request);

            Assert.IsTrue(response.IsSuccess);
            var loginCount = await Db.Set<LoginEntity>().CountAsync(x => x.EmployeeId == employee.Id);
            Assert.AreEqual(1, loginCount);
        }

        [TestMethod]
        public async Task LoginReturnsJwtTokenOnSuccess()
        {
            var employee = new EmployeeEntity(default, "a8304980283rtyu", "Test", "test@nomail.com", "07482918302", 1L, "9ppucAgOQ+oPGYetZB/Hxbktph6vVBQHhu+2agNEFoOiM7UpYgw/6r13KZ6gmTYkn31is53pPxW4Krp1jbVOo2YwOeNfj2aov2F1P4gvd+orot/3bBhKl81bQ3Xrw2cERjvSFHoYpvhMvD/VzpJ0QEjlJHrrLmdL3CzIBxUhNDo=", 0M);
            await Db.AddAsync(employee);
            await Db.SaveChangesAsync();

            var request = new LoginRequestModel { Id = employee.Id, PIN = "0000", KioskId = "KIOSK01" };
            var response = await Service.LoginAsync(request);

            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(employee.Name, response.Data.Name);
            Assert.AreNotEqual(string.Empty, response.Data.Token.ToString());

            var tokenPayload = Jwt.Decode(response.Data.Token.ToString());
            Assert.AreEqual(6, tokenPayload.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task BalanceThrowsArgumentOutOfRangeExceptionForZeroEmployeeId()
        {
            await Service.BalanceAsync(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task BalanceThrowsArgumentOutOfRangeExceptionForNegativeEmployeeId()
        {
            await Service.BalanceAsync(-2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task BalanceThrowsInvalidOperationExceptionWhenEmployeeNotFound()
        {
            await Service.BalanceAsync(10);
        }
    }
}