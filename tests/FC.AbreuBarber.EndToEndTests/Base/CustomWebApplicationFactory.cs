
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FC.AbreuBarber.EndToEndTests.Base
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup>
        where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbOptions = services.FirstOrDefault(x =>
                    x.ServiceType == typeof(DbContextOptions<AbreuBarberDbContext>
                ));

                if (dbOptions != null)
                {
                    services.Remove(dbOptions);
                }

                services.AddDbContext<AbreuBarberDbContext>(options =>
                {
                    options.UseInMemoryDatabase("end2end-tests-db");
                });
            });

            base.ConfigureWebHost(builder);
        }
    }
}
