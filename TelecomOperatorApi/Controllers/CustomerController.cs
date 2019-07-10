using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            {
                return BadRequest();
            }

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
    }
}
