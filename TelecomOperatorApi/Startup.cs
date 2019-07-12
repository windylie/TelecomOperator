using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelecomOperatorApi.Entities;
using TelecomOperatorApi.Repository;

namespace TelecomOperatorApi
{
    public class Startup
    {
        private readonly string AllowFrontendWebOrigins = "_allowFrontendWebOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowFrontendWebOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connectionString = Configuration["connectionStrings:telecomOperatorInfoDBConnectionString"];
            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<TelecomOperatorContext>(o => o.UseNpgsql(connectionString));

            services.AddScoped<IPhoneInfoRepository, PhoneInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TelecomOperatorContext telecomOperatorContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            telecomOperatorContext.EnsureSeedDataForContext();

            app.UseCors(AllowFrontendWebOrigins);
            app.UseMvc();
        }
    }
}
