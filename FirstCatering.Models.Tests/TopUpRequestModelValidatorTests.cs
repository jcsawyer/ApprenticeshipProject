using FirstCatering.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace FirstCatering.Models.Tests
{
    [TestClass]
    public class TopUpRequestModelValidatorTests
    {
        public TopUpRequestModelValidatorTests() { }

        [TestMethod]
        public async Task ZeroAmountReturnsCorrectError()
        {
            var request = new TopUpRequestModel() { Amount = 0M };
            var validation = await new TopUpRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Cannot top up zero or negative amount", validation.Message);
        }

        [TestMethod]
        public async Task NegativeAmountReturnsCorrectError()
        {
            var request = new TopUpRequestModel() { Amount = -20M };
            var validation = await new TopUpRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Cannot top up zero or negative amount", validation.Message);
        }

        [TestMethod]
        public async Task ValidAmountIsValidatedSuccess()
        {
            var request = new TopUpRequestModel() { Amount = 10M };
            var validation = await new TopUpRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsSuccess);
        }

        [TestMethod]
        public async Task NineDigitAmountReturnsCorrectError()
        {
            var request = new TopUpRequestModel() { Amount = 123456789M };
            var validation = await new TopUpRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Amount is maximum 8 digits and maximum 2 decimal places", validation.Message);
        }

        [TestMethod]
        public async Task ThreeDecimalPlacesAmountReturnsCorrectError()
        {
            var request = new TopUpRequestModel() { Amount = 10.543M };
            var validation = await new TopUpRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("Amount is maximum 8 digits and maximum 2 decimal places", validation.Message);
        }

        [TestMethod]
        public async Task TwoDecimalPlacesAmountIsValidatedSuccess()
        {
            var request = new TopUpRequestModel() { Amount = 10.52M };
            var validation = await new TopUpRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsSuccess);
        }
    }
}
