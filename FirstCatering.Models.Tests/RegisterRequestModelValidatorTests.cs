using FirstCatering.Models.Employee;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace FirstCatering.Models.Tests
{
    [TestClass]
    public class RegisterRequestModelValidatorTests
    {
        public RegisterRequestModelValidatorTests() { }

        [TestMethod]
        public async Task AllEmptyReturnsCorrectError()
        {
            var request = new RegisterRequestModel();
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("EmployeeId is required\nName is required\nEmail is required\nMobile is required\nCompanyId is required\nPIN is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyEmployeeIdReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { Name = "Test", Email = "test@nomail.com", MobileNumber = "07429483273", CompanyId = 1L, PIN = "0000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("EmployeeId is required", validation.Message);
        }

        [TestMethod]
        public async Task InvalidEmployeeIdReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "&", Name = "Test", Email = "test@nomail.com", MobileNumber = "07429483273", CompanyId = 1L, PIN = "0000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("EmployeeId must be valid", validation.Message);
        }

        [TestMethod]
        public async Task EmptyNameReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "kd8e3jn59sjeqwm4", Email = "test@nomail.com", MobileNumber = "07429483273", CompanyId = 1L, PIN = "0000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Name is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyEmailReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "kd8e3jn59sjeqwm4", Name = "Test", MobileNumber = "07429483273", CompanyId = 1L, PIN = "0000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Email is required", validation.Message);
        }

        [TestMethod]
        public async Task InvalidEmailReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "kd8e3jn59sjeqwm4", Email = "nomail", Name = "Test", MobileNumber = "07429483273", CompanyId = 1L, PIN = "0000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Email must be a valid email address", validation.Message);
        }

        [TestMethod]
        public async Task ValidEmailDoesNotReturnError()
        {
            var request = new RegisterRequestModel() { Email = "test@nomail.com" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("EmployeeId is required\nName is required\nMobile is required\nCompanyId is required\nPIN is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyMobileReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "kd8e3jn59sjeqwm4", Email = "test@nomail.com", Name = "Test", CompanyId = 1L, PIN = "0000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Mobile is required", validation.Message);
        }

        [TestMethod]
        public async Task InvalidMobileReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "kd8e3jn59sjeqwm4", Email = "test@nomail.com", Name = "Test", MobileNumber = "1", CompanyId = 1L, PIN = "0000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("MobileNumber must be a valid UK mobile phone number", validation.Message);
        }

        [TestMethod]
        public async Task ValidMobileReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { MobileNumber = "07429374103" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("EmployeeId is required\nName is required\nEmail is required\nCompanyId is required\nPIN is required", validation.Message);
        }

        [TestMethod]
        public async Task ValidCountryCodeMobileReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { MobileNumber = "447429374103" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("EmployeeId is required\nName is required\nEmail is required\nCompanyId is required\nPIN is required", validation.Message);
        }

        [TestMethod]
        public async Task ValidPlusCountryCodeMobileReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { MobileNumber = "+447429374103" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("EmployeeId is required\nName is required\nEmail is required\nCompanyId is required\nPIN is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyCompanyIdReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "kd8e3jn59sjeqwm4", Email = "test@nomail.com", MobileNumber = "07429937428", Name = "Test", PIN = "0000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("CompanyId is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyPINReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "kd8e3jn59sjeqwm4", Email = "test@nomail.com", MobileNumber = "07429937428", Name = "Test", CompanyId = 1L };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("PIN is required", validation.Message);
        }

        [TestMethod]
        public async Task TooLongPINReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "kd8e3jn59sjeqwm4", Email = "test@nomail.com", MobileNumber = "07429937428", Name = "Test", CompanyId = 1L, PIN = "00000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Security PIN must be valid", validation.Message);
        }

        [TestMethod]
        public async Task TooShortPINReturnsCorrectError()
        {
            var request = new RegisterRequestModel() { EmployeeId = "kd8e3jn59sjeqwm4", Email = "test@nomail.com", MobileNumber = "07429937428", Name = "Test", CompanyId = 1L, PIN = "000" };
            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Security PIN must be valid", validation.Message);
        }
    }
}
