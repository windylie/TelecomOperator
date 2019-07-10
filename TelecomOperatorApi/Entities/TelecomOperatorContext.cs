using Microsoft.EntityFrameworkCore;

namespace TelecomOperatorApi.Entities
{
    public class TelecomOperatorContext : DbContext
    {
        public TelecomOperatorContext(DbContextOptions<TelecomOperatorContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Phone> Phones { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
