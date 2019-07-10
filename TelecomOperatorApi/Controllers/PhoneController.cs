using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TelecomOperatorApi.Dtos;
using TelecomOperatorApi.Repository;

namespace TelecomOperatorApi.Controllers
{
    [Route("api/phones")]
    public class PhoneController : Controller
    {
        private IPhoneInfoRepository _phoneInfoRepository;
        public PhoneController(IPhoneInfoRepository phoneInfoRepository)
        {
            _phoneInfoRepository = phoneInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetAllPhoneNumbers()
        {
            var phoneEntities = _phoneInfoRepository.GetAllPhoneNumbers()
                .Select(p => 
                    new PhoneDto()
                    {
                        Id = p.Id,
                        CustomerId = p.CustomerId,
                        PhoneNo = p.Number,
                        Activated = p.Activated
                    })
                .ToList();
            
            return Ok(phoneEntities);
        }
        
    }
}
