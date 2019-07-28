using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using TelecomOperatorApi.Controllers;
using TelecomOperatorApi.Dtos;
using TelecomOperatorApi.Entities;
using TelecomOperatorApi.Repository;

namespace TelecomOperatorApiTests.ControllerUnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private IPhoneInfoRepository _phoneInfoRepository;
        private CustomerController _customerController;

        [SetUp]
        public void Setup()
        {
            _phoneInfoRepository = Substitute.For<IPhoneInfoRepository>();
            _customerController = new CustomerController(_phoneInfoRepository);
        }

        #region Test_get_customers
        [Test]
        public void When_get_all_customers_then_return_ok()
        {
            var result = _customerController.GetAllCustomers();
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);

            _phoneInfoRepository.GetAllCustomers().Received();
        }

        [Test]
        public void When_get_all_customers_phone_numbers_then_return_ok()
        {
            var result = _customerController.GetAllCustomerPhoneNumbers(1);
            Assert.IsInstanceOf<OkObjectResult>(result);

            _phoneInfoRepository.GetCustomerPhoneNumbers(1).Received();
        }
        #endregion

        #region Test_create_phone_number
        [Test]
        public void When_user_input_null_phone_no_when_create_phone_number_then_return_bad_request()
        {
            var createPhoneDto = new CreatePhoneRequestDto()
            {
                PhoneNo = null
            };

            var result = _customerController.CreatePhoneNumber(1, createPhoneDto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badResult = result as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            var value = badResult.Value as OperationResponse;
            Assert.IsFalse(value.IsSuccessful);
        }

        [Test]
        public void When_create_phone_number_for_non_existance_customer_then_return_bad_request()
        {
            _phoneInfoRepository.IsCustomerExists(1).Returns(false);

            var createPhoneDto = new CreatePhoneRequestDto()
            {
                PhoneNo = "0412345678"
            };

            var result = _customerController.CreatePhoneNumber(1, createPhoneDto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void When_duplicate_phone_data_entered_during_create_phone_number_then_return_bad_request()
        {
            var createPhoneDto = new CreatePhoneRequestDto()
            {
                PhoneNo = "0412345678"
            };

            _phoneInfoRepository.IsDuplicatePhoneNumberFound(createPhoneDto.PhoneNo).Returns(true);

            var result = _customerController.CreatePhoneNumber(1, createPhoneDto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [TestCase("0412345")]
        [TestCase("9876543211")]
        [TestCase("random")]
        public void When_invalid_phone_number_entered_during_create_phone_number_then_return_bad_request(string phoneNo)
        {
            var createPhoneDto = new CreatePhoneRequestDto()
            {
                PhoneNo = phoneNo
            };

            var result = _customerController.CreatePhoneNumber(1, createPhoneDto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void When_valid_data_entered_then_new_phone_added_into_the_context()
        {
            var createPhoneDto = new CreatePhoneRequestDto()
            {
                PhoneNo = "0412345678"
            };

            _phoneInfoRepository.IsCustomerExists(1).Returns(true);
            _phoneInfoRepository.IsDuplicatePhoneNumberFound(createPhoneDto.PhoneNo).Returns(false);

            var result = _customerController.CreatePhoneNumber(1, createPhoneDto);

            _phoneInfoRepository.Received().CreatePhoneNumber(Arg.Any<Phone>());
        }

        [Test]
        public void When_new_phone_successfully_save_into_db_then_return_success()
        {
            var createPhoneDto = new CreatePhoneRequestDto()
            {
                PhoneNo = "0412345678"
            };

            _phoneInfoRepository.IsCustomerExists(1).Returns(true);
            _phoneInfoRepository.IsDuplicatePhoneNumberFound(createPhoneDto.PhoneNo).Returns(false);
            _phoneInfoRepository.Save().Returns(true);

            var result = _customerController.CreatePhoneNumber(1, createPhoneDto);
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);

            var routeResult = result as CreatedAtRouteResult;
            Assert.AreEqual(201, routeResult.StatusCode);

            var value = routeResult.Value as OperationResponse;
            Assert.IsTrue(value.IsSuccessful);

        }

        [Test]
        public void When_new_phone_unsuccessfully_save_into_db_then_return_server_error()
        {
            var createPhoneDto = new CreatePhoneRequestDto()
            {
                PhoneNo = "0412345678"
            };

            _phoneInfoRepository.IsCustomerExists(1).Returns(true);
            _phoneInfoRepository.IsDuplicatePhoneNumberFound(createPhoneDto.PhoneNo).Returns(false);
            _phoneInfoRepository.Save().Returns(false);

            var result = _customerController.CreatePhoneNumber(1, createPhoneDto);
            Assert.IsInstanceOf<ObjectResult>(result);

            var statusCodeResult = result as ObjectResult;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region Test_activate_phone_number
        [Test]
        public void When_activate_phone_number_for_non_existance_customer_then_return_bad_request()
        {
            _phoneInfoRepository.IsCustomerExists(1).Returns(false);

            var result = _customerController.ActivatePhoneNumber(1, "0412345678");
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void When_activate_phone_number_for_non_existance_number_then_return_bad_request()
        {
            _phoneInfoRepository.IsCustomerExists(1).Returns(true);
            _phoneInfoRepository.GetCustomerPhoneNumbers(1).Returns(new List<Phone>());

            var result = _customerController.ActivatePhoneNumber(1, "0412345678");
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void When_activate_phone_number_then_number_is_activated()
        {
            var phoneDto = new Phone()
            {
                Id = 1,
                CustomerId = 1,
                Customer = new Customer() { Id = 1, Name = "Maria" },
                Number = "0412345678",
                Activated = false
            };

            _phoneInfoRepository.IsCustomerExists(1).Returns(true);
            _phoneInfoRepository.GetCustomerPhoneNumbers(1).Returns(new List<Phone>() { phoneDto });

            var result = _customerController.ActivatePhoneNumber(phoneDto.CustomerId, phoneDto.Number);

            _phoneInfoRepository.Received().ActivatePhoneNumber(phoneDto.CustomerId, phoneDto.Number);
        }

        [Test]
        public void When_number_is_activated_successfully_in_db_then_return_success()
        {
            var phoneDto = new Phone()
            {
                Id = 1,
                CustomerId = 1,
                Customer = new Customer() { Id = 1, Name = "Maria" },
                Number = "0412345678",
                Activated = false
            };

            _phoneInfoRepository.IsCustomerExists(1).Returns(true);
            _phoneInfoRepository.GetCustomerPhoneNumbers(1).Returns(new List<Phone>() { phoneDto });
            _phoneInfoRepository.Save().Returns(true);

            var result = _customerController.ActivatePhoneNumber(phoneDto.CustomerId, phoneDto.Number);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void When_number_is_activated_unsuccessfully_in_db_then_return_server_error()
        {
            var phoneDto = new Phone()
            {
                Id = 1,
                CustomerId = 1,
                Customer = new Customer() { Id = 1, Name = "Maria" },
                Number = "0412345678",
                Activated = false
            };

            _phoneInfoRepository.IsCustomerExists(1).Returns(true);
            _phoneInfoRepository.GetCustomerPhoneNumbers(1).Returns(new List<Phone>() { phoneDto });
            _phoneInfoRepository.Save().Returns(false);

            var result = _customerController.ActivatePhoneNumber(phoneDto.CustomerId, phoneDto.Number);
            Assert.IsInstanceOf<ObjectResult>(result);

            var statusCodeResult = result as ObjectResult;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
        #endregion
    }
}
