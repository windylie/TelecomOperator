using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TelecomOperatorApi.Models;

namespace TelecomOperatorApi.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        [HttpGet("{customerId}/phones", Name = "GetCustomerPhones")]
        public IActionResult GetAllCustomerPhoneNumbers(int customerId)
        {
            var phonesToReturn = PhoneDataStore.Current.AllPhoneNumbers.Where(c => c.CustomerId == customerId);

            return Ok(phonesToReturn);
        }

        [HttpPost("{customerId}/phones")]
        public IActionResult CreatePhoneNumber(int customerId, [FromBody] CreatePhoneRequestDto newPhoneNo)
        {
            if (newPhoneNo == null)
                return BadRequest();

            var isCustomerExists = CustomerDataStore.Current.Customers.FirstOrDefault(c => c.Id == customerId) != null;
            if (!isCustomerExists)
                return BadRequest();

            var isDuplicatePhoneNo = PhoneDataStore.Current.AllPhoneNumbers.Any(p => p.PhoneNo == newPhoneNo.PhoneNo);
            if (isDuplicatePhoneNo)
                ModelState.AddModelError("PhoneNo", "This phone number already exists");

            var isPhoneNoValid = Regex.IsMatch(newPhoneNo.PhoneNo, "(03|04)\\d{8}$");
            if (!isPhoneNoValid)
                ModelState.AddModelError("PhoneNo", "Phone number is incorrect. Enter 10 digits number beginning with 03 or 04.");

            if (!ModelState.IsValid)
                return BadRequest();

            var maxPhoneId = PhoneDataStore.Current.AllPhoneNumbers.Max(p => p.Id);
            var newPhoneData = new PhoneDto()
            {
                Id = ++maxPhoneId,
                CustomerId = customerId,
                PhoneNo = newPhoneNo.PhoneNo,
                Activated = false
            };

            PhoneDataStore.Current.AllPhoneNumbers.Add(newPhoneData);

            return CreatedAtRoute("GetCustomerPhones", new { customerId = customerId, newPhoneNo = newPhoneData.PhoneNo }, newPhoneData);
        }

        [HttpPut("{customerId}/phones/{phoneNumber}/activate")]
        public IActionResult ActivatePhoneNumber(int customerId, string phoneNumber)
        {
            var isCustomerExists = CustomerDataStore.Current.Customers.FirstOrDefault(c => c.Id == customerId) != null;
            if (!isCustomerExists)
                return BadRequest();

            var phoneNumberToActivate = PhoneDataStore.Current.AllPhoneNumbers.Where(c => c.CustomerId == customerId).FirstOrDefault(p => p.PhoneNo == phoneNumber);
            if (phoneNumberToActivate == null)
                return BadRequest();

            if (phoneNumberToActivate.Activated == false)
                phoneNumberToActivate.Activated = true;

            return NoContent();
        }

        [HttpPut("{customerId}/phones/{phoneNumber}/deactivate")]
        public IActionResult DeactivatePhoneNumber(int customerId, string phoneNumber)
        {
            var isCustomerExists = CustomerDataStore.Current.Customers.FirstOrDefault(c => c.Id == customerId) != null;
            if (!isCustomerExists)
                return BadRequest();

            var phoneNumberToDectivate = PhoneDataStore.Current.AllPhoneNumbers.Where(c => c.CustomerId == customerId).FirstOrDefault(p => p.PhoneNo == phoneNumber);
            if (phoneNumberToDectivate == null)
                return BadRequest();

            if (phoneNumberToDectivate.Activated == true)
                phoneNumberToDectivate.Activated = false;

            return NoContent();
        }
    }
}
