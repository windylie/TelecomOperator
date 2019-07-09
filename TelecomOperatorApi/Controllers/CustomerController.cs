using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelecomOperatorApi.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        [HttpGet("{customerId}/phones")]
        public IActionResult GetAllCustomerPhoneNumbers(int customerId)
        {
            var phonesToReturn = PhoneDataStore.Current.AllPhoneNumbers.Where(c => c.CustomerId == customerId);

            return Ok(phonesToReturn);
        }
    }
}
