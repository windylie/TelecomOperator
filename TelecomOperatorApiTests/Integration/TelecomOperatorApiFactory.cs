using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using TelecomOperatorApi.Entities;

namespace TelecomOperatorApiTests.Integration
{
    public class TelecomOperatorApiFactory : WebApplicationFactory<TelecomOperatorApi.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseEnvironment("Test");
        }
    }
}
