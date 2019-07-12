using System.Collections.Generic;
using TelecomOperatorApi.Entities;

namespace TelecomOperatorApi.Repository
{
    public interface IPhoneInfoRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Phone> GetAllPhoneNumbers();
        IEnumerable<Phone> GetCustomerPhoneNumbers(int customerId);
        bool IsCustomerExists(int customerId);
        bool IsDuplicatePhoneNumberFound(string phoneNo);
        void CreatePhoneNumber(Phone newPhoneData);
        void ActivatePhoneNumber(int customerId, string phoneNo);
        bool Save();
    }
}
