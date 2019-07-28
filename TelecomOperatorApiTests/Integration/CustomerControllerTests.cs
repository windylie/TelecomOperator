using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TelecomOperatorApi.Entities;

namespace TelecomOperatorApiTests.Integration
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private TelecomOperatorApiFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new TelecomOperatorApiFactory();
            
            _client = _factory.CreateClient();

            var context = (TelecomOperatorContext)_factory.Server.Host.Services.GetService(typeof(TelecomOperatorContext));
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.EnsureSeedDataForContext();
        }

        [Test]
        public async Task When_client_request_get_all_customers_then_the_result_is_ok_and_correct_data_are_returned()
        {
            //Act
            var getAllCustomersResult = await _client.GetAsync("/api/customers", HttpCompletionOption.ResponseContentRead);

            //Assert
            getAllCustomersResult.EnsureSuccessStatusCode();

            var getAllCustomersContent = await getAllCustomersResult.Content.ReadAsStringAsync();
            var content = JArray.Parse(getAllCustomersContent);

            var customerIds = content.Children<JObject>().Select(c => c["id"]);
            Assert.IsTrue(customerIds.Count() > 0);
        }

        [Test]
        public async Task When_client_create_phone_number_then_the_result_is_ok_and_correct_data_are_returned()
        {
            //Act
            var customerId = 2;
            string createPhoneNoJson = "{'phoneNo':'0412345678'}";
            var body = new StringContent(createPhoneNoJson, Encoding.UTF8, "application/json");

            var createPhoneResult = await _client.PostAsync(string.Format("/api/customers/{0}/phones", customerId), body);

            //Assert
            createPhoneResult.EnsureSuccessStatusCode();

            var createPhoneContent = await createPhoneResult.Content.ReadAsStringAsync();
            var content = JObject.Parse(createPhoneContent);
            Assert.IsTrue(content["isSuccessful"].Value<bool>());
            Assert.AreEqual("0412345678", content["data"]["newPhoneNo"].ToString());

            //Act
            createPhoneNoJson = "{'phoneNo':'0487654321'}";
            body = new StringContent(createPhoneNoJson, Encoding.UTF8, "application/json");
            await _client.PostAsync(string.Format("/api/customers/{0}/phones", customerId), body);
            var getCustPhonesResult = await _client.GetAsync(string.Format("/api/customers/{0}/phones", customerId), HttpCompletionOption.ResponseContentRead);

            //Assert
            getCustPhonesResult.EnsureSuccessStatusCode();

            var getCustPhonesContent = await getCustPhonesResult.Content.ReadAsStringAsync();
            var customerPhones = JArray.Parse(getCustPhonesContent);
            Assert.AreEqual(2, customerPhones.Count());
        }

        [Test]
        public async Task When_client_activate_phone_number_then_the_result_is_ok_and_phone_is_activated()
        {
            //Arrange
            var customerId = 3;
            var phoneNo = "0312345678";
            string createPhoneNoJson = "{'phoneNo':'0312345678'}";
            var body = new StringContent(createPhoneNoJson, Encoding.UTF8, "application/json");

            await _client.PostAsync(string.Format("/api/customers/{0}/phones", customerId), body);

            //Assert
            var getCustPhonesResult = await _client.GetAsync(string.Format("/api/customers/{0}/phones", customerId), HttpCompletionOption.ResponseContentRead);
            var getCustPhonesContent = await getCustPhonesResult.Content.ReadAsStringAsync();
            var customerPhones = JArray.Parse(getCustPhonesContent);
            Assert.IsFalse(customerPhones[0]["activated"].Value<bool>());

            //Act
            var result = await _client.PutAsync(string.Format("/api/customers/{0}/phones/{1}/activation", customerId, phoneNo), body);

            //Assert
            result.EnsureSuccessStatusCode();

            getCustPhonesResult = await _client.GetAsync(string.Format("/api/customers/{0}/phones", customerId), HttpCompletionOption.ResponseContentRead);
            getCustPhonesContent = await getCustPhonesResult.Content.ReadAsStringAsync();
            customerPhones = JArray.Parse(getCustPhonesContent);
            Assert.IsTrue(customerPhones[0]["activated"].Value<bool>());
        }
    }
}
