using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelecomOperatorApi.Models;

namespace TelecomOperatorApi
{
    public class CustomerDataStore
    {
        public static CustomerDataStore Current { get; } = new CustomerDataStore();
        public List<CustomerDto> Customers { get; set; }

        public CustomerDataStore()
        {
            Customers = new List<CustomerDto>()
            {
                new CustomerDto()
                {
                    Id = 1,
                    Name = "Customer 1"
                },
                new CustomerDto()
                {
                    Id = 2,
                    Name = "Customer 2"
                },
            };
        }
    }
}
