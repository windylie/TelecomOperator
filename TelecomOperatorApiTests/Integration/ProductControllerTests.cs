using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TelecomOperatorApi.Entities;

namespace TelecomOperatorApiTests.Integration
{
    [TestFixture]
    public class ProductControllerTests
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
        public async Task When_client_request_get_all_phoneNumbers_then_the_result_is_ok_and_correct_data_are_returned()
        {
            //Arrange
            await CreatePhoneNumber(1, "0422223333");
            await CreatePhoneNumber(1, "0433332222");
            await CreatePhoneNumber(2, "0499998888");
            await CreatePhoneNumber(3, "0366665555");

            //Act
            var getAllPhonesResult = await _client.GetAsync("/api/phones", HttpCompletionOption.ResponseContentRead);

            //Assert
            getAllPhonesResult.EnsureSuccessStatusCode();

            var getAllPhonesContent = await getAllPhonesResult.Content.ReadAsStringAsync();
            var content = JArray.Parse(getAllPhonesContent);
            Assert.AreEqual(4, content.Count);
        }

        private async Task CreatePhoneNumber(int customerId, string phoneNo)
        {
            //Act
            string createPhoneNoJson = "{'phoneNo':'" + phoneNo + "'}";
            var body = new StringContent(createPhoneNoJson, Encoding.UTF8, "application/json");

            await _client.PostAsync(string.Format("/api/customers/{0}/phones", customerId), body);
        }
    }
}
