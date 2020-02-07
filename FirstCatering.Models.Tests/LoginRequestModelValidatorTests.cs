using FirstCatering.Models.Employee;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace FirstCatering.Models.Tests
{
    [TestClass]
    public class LoginRequestModelValidatorTests
    {
        public LoginRequestModelValidatorTests() { }

        [TestMethod]
        public async Task EmptyIdAndPINAndKioskIdReturnsCorrectError()
        {
            var request = new LoginRequestModel();
            var validation = await new LoginRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Id is required\nPIN is required\nKioskId is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyIdAndPINReturnsCorrectError()
        {
            var request = new LoginRequestModel() { KioskId = "KIOSK01" };
            var validation = await new LoginRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Id is required\nPIN is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyIdAndKioskIdReturnsCorrectError()
        {
            var request = new LoginRequestModel() { PIN = "0000" };
            var validation = await new LoginRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Id is required\nKioskId is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyPINAndKioskIdReturnsCorrectError()
        {
            var request = new LoginRequestModel() { Id = 1L };
            var validation = await new LoginRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("PIN is required\nKioskId is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyIdReturnsCorrectError()
        {
            var request = new LoginRequestModel() { PIN = "0000", KioskId = "KIOSK01" };
            var validation = await new LoginRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Id is required", validation.Message);
        }

        [TestMethod]
        public async Task PINTooLongReturnsCorrectError()
        {
            var request = new LoginRequestModel() { Id = 1L, PIN = "00000", KioskId = "KIOSK01" };
            var validation = await new LoginRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("PIN must be valid", validation.Message);
        }

        [TestMethod]
        public async Task PINTooShortReturnsCorrectError()
        {
            var request = new LoginRequestModel() { Id = 1L, PIN = "000", KioskId = "KIOSK01" };
            var validation = await new LoginRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("PIN must be valid", validation.Message);
        }
    }
}
