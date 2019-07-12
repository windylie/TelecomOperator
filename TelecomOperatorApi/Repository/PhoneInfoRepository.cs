using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelecomOperatorApi.Entities;

namespace TelecomOperatorApi.Repository
{
    public class PhoneInfoRepository : IPhoneInfoRepository
    {
        private TelecomOperatorContext _context;
        public PhoneInfoRepository(TelecomOperatorContext context)
        {
            _context = context;
        }

        public void CreatePhoneNumber(Phone newPhoneData)
        {
            _context.Phones.Add(newPhoneData);
        }

        public void ActivatePhoneNumber(int customerId, string phoneNo)
        {
            var phoneToActivate = _context.Phones.FirstOrDefault(p => p.CustomerId == customerId && p.Number == phoneNo);
            phoneToActivate.Activated = true;
        }

        public IEnumerable<Phone> GetAllPhoneNumbers()
        {
            return _context.Phones.Include(p => p.Customer).OrderBy(p => p.CustomerId).ToList();
        }

        public IEnumerable<Phone> GetCustomerPhoneNumbers(int customerId)
        {
            return _context.Phones.Where(p => p.CustomerId == customerId).ToList();
        }

        public bool IsCustomerExists(int customerId)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == customerId) != null;
        }

        public bool IsDuplicatePhoneNumberFound(string phoneNo)
        {
            return _context.Phones.Any(p => p.Number == phoneNo);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.OrderBy(c => c.Id).ToList();
        }
    }
}
