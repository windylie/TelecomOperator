using System.Collections.Generic;
using System.Linq;

namespace TelecomOperatorApi.Entities
{
    public static class TelecomOperatorExtension
    {
        public static void EnsureSeedDataForContext(this TelecomOperatorContext context)
        {
            if (context.Customers.Any())
            {
                return;
            }

            var customers = new List<Customer>()
            {
                new Customer { Name = "Mario Speedwagon" },
                new Customer { Name = "Peter Cruiser" },
                new Customer { Name = "Anna Sthesia" },
                new Customer { Name = "Anna Mull" },
                new Customer { Name = "Paul Molive" },
                new Customer { Name = "Paige Turner" },
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
