using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fc.AbreuBarber.Api.Configurations
{
    public static class ConnectionsConfiguration
    {

        public static IServiceCollection AddCustomConnections(
            this IServiceCollection services)
        {

            services.AddDbConnection();

            return services;
        }


        private static IServiceCollection AddDbConnection(
            this IServiceCollection services)
        {

            services.AddDbContext<AbreuBarberDbContext>(
                options =>
                    options.UseInMemoryDatabase("InMemory-DSV-Database")
                );

            return services;
        }
    }
}
