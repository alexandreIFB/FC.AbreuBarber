using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fc.AbreuBarber.Api.Configurations
{
    public static class ConnectionsConfiguration
    {

        public static IServiceCollection AddAppConections(
        this IServiceCollection services,
        IConfiguration configuration
    )
        {
            services.AddDbConnection(configuration);
            return services;
        }

        private static IServiceCollection AddDbConnection(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration
                .GetConnectionString("AbreuBarberDb");
            services.AddDbContext<AbreuBarberDbContext>(
                options => options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                )
            );
            return services;
        }
    }
}
