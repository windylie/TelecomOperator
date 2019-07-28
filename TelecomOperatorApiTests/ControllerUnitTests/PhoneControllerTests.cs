using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using TelecomOperatorApi.Controllers;
using TelecomOperatorApi.Repository;

namespace TelecomOperatorApiTests.ControllerUnitTests
{
    [TestFixture]
    public class PhoneControllerTests
    {
        private IPhoneInfoRepository _phoneInfoRepository;
        private PhoneController _phoneController;

        [SetUp]
        public void Setup()
        {
            _phoneInfoRepository = Substitute.For<IPhoneInfoRepository>();
            _phoneController = new PhoneController(_phoneInfoRepository);
        }

        [Test]
        public void When_get_all_phone_numbers_then_return_ok()
        {
            var result = _phoneController.GetAllPhoneNumbers();
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);

            _phoneInfoRepository.GetAllPhoneNumbers().Received();
        }
    }
}
