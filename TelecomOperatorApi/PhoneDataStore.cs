using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelecomOperatorApi.Models;

namespace TelecomOperatorApi
{
    public class PhoneDataStore
    {
        public static PhoneDataStore Current { get; } = new PhoneDataStore();
        public List<PhoneDto> AllPhoneNumbers { get; set; } 

        public PhoneDataStore()
        {
            AllPhoneNumbers = new List<PhoneDto>()
            {
                new PhoneDto()
                {
                    Id = 1,
                    CustomerId = 1,
                    PhoneNo = "1234567890",
                    Activated = true
                },
                new PhoneDto()
                {
                    Id = 2,
                    CustomerId = 2,
                    PhoneNo = "1111111111",
                    Activated = true
                },
                new PhoneDto()
                {
                    Id = 3,
                    CustomerId = 2,
                    PhoneNo = "2222222222",
                    Activated = false
                },
            };
        }
    }
}
