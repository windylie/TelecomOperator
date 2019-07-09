using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TelecomOperatorApi.Controllers
{
    [Route("api/phones")]
    public class PhoneController : Controller
    {
        [HttpGet()]
        public IActionResult GetAllPhoneNumbers()
        {
            return Ok(PhoneDataStore.Current.AllPhoneNumbers);
        }
        
    }
}
