using FirstCatering.Models.Employee;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace FirstCatering.Models.Tests
{
    [TestClass]
    public class IdentifyRequestModelValidatorTests
    {
        public IdentifyRequestModelValidatorTests() { }

        [TestMethod]
        public async Task EmptyCompanyIdAndEmployeeIdReturnsCorrectErrors()
        {
            var request = new IdentifyRequestModel();
            var validation = await new IdentifyRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("CompanyId is required\nEmployeeId is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyCompanyIdAndInvalidEmployeeIdReturnsCorrectError()
        {
            var request = new IdentifyRequestModel() { EmployeeId = "&" };
            var validation = await new IdentifyRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("CompanyId is required\nEmployeeId must be valid", validation.Message);
        }

        [TestMethod]
        public async Task EmptyCompanyIdReturnsCorrectError()
        {
            var request = new IdentifyRequestModel() { EmployeeId = "ld84jd9sn2yedk54" };
            var validation = await new IdentifyRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("CompanyId is required", validation.Message);
        }

        [TestMethod]
        public async Task EmptyEmployeeIdReturnsCorrectError()
        {
            var request = new IdentifyRequestModel() { CompanyId = 1L };
            var validation = await new IdentifyRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("EmployeeId is required", validation.Message);
        }

        [TestMethod]
        public async Task InvalidEmployeeIdReturnsCorrectError()
        {
            var request = new IdentifyRequestModel() { CompanyId = 1L, EmployeeId = "&" };
            var validation = await new IdentifyRequestModelValidator().ValidateAsync(request);

            Assert.IsTrue(validation.IsError);
            Assert.AreEqual("EmployeeId must be valid", validation.Message);
        }
    }
}
