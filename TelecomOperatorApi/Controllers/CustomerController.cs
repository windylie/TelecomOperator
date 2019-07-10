using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;
using TelecomOperatorApi.Entities;
using TelecomOperatorApi.Dtos;
using TelecomOperatorApi.Repository;

namespace TelecomOperatorApi.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private IPhoneInfoRepository _phoneInfoRepository;
        public CustomerController(IPhoneInfoRepository phoneInfoRepository)
        {
            _phoneInfoRepository = phoneInfoRepository;
        }

        [HttpGet("{customerId}/phones", Name = "GetCustomerPhones")]
        public IActionResult GetAllCustomerPhoneNumbers(int customerId)
        {
            var phonesToReturn = _phoneInfoRepository.GetCustomerPhoneNumbers(customerId)
                .Select(p =>
                    new PhoneDto()
                    {
                        Id = p.Id,
                        CustomerId = p.CustomerId,
                        PhoneNo = p.Number,
                        Activated = p.Activated
                    })
                .ToList();

            return Ok(phonesToReturn);
        }

        [HttpPost("{customerId}/phones")]
        public IActionResult CreatePhoneNumber(int customerId, [FromBody] CreatePhoneRequestDto newPhoneNo)
        {
            if (newPhoneNo == null)
                return BadRequest(OperationResponse.Fail("Phone number is required"));

            if (!_phoneInfoRepository.IsCustomerExists(customerId))
                return BadRequest(OperationResponse.Fail("Customer not found"));

            if (_phoneInfoRepository.IsDuplicatePhoneNumberFound(newPhoneNo.PhoneNo))
                return BadRequest(OperationResponse.Fail("This phone number already exists"));

            var isPhoneNoValid = Regex.IsMatch(newPhoneNo.PhoneNo, "(03|04)\\d{8}$");
            if (!isPhoneNoValid)
                return BadRequest(OperationResponse.Fail("Phone number is incorrect! Enter 10 digits number beginning with 03 or 04!"));

            var newPhoneData = new Phone()
            {
                CustomerId = customerId,
                Number = newPhoneNo.PhoneNo,
                Activated = false
            };

            _phoneInfoRepository.CreatePhoneNumber(newPhoneData);
            if (!_phoneInfoRepository.Save())
            {
                return StatusCode(500, OperationResponse.Fail("Unsuccessful saving data. Something wrong in the server."));
            }

            return CreatedAtRoute("GetCustomerPhones",
                customerId,
                OperationResponse.Succeed(new
                {
                    customerId,
                    newPhoneNo = newPhoneData.Number
                }));
        }

        [HttpPut("{customerId}/phones/{phoneNumber}/activation")]
        public IActionResult ActivatePhoneNumber(int customerId, string phoneNumber)
        {
            if (!_phoneInfoRepository.IsCustomerExists(customerId))
                return BadRequest(OperationResponse.Fail("Customer not found"));

            var phoneNumberToActivate = _phoneInfoRepository.GetCustomerPhoneNumbers(customerId).FirstOrDefault(p => p.Number == phoneNumber);
            if (phoneNumberToActivate == null)
                return BadRequest(OperationResponse.Fail("Customer does not have this number! Please input correct number!"));

            _phoneInfoRepository.ActivatePhoneNumber(customerId, phoneNumber);
            if (!_phoneInfoRepository.Save())
            {
                return StatusCode(500, OperationResponse.Fail("Unsuccessful saving data. Something wrong in the server."));
            }

            return Ok(OperationResponse.Succeed());
        }
    }
}
