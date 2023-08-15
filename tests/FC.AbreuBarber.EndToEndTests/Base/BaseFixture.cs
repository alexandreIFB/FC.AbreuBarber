
using Bogus;
using Fc.AbreuBarber.Api;
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FC.AbreuBarber.EndToEndTests.Base
{
    public class BaseFixture
    {
        public ApiClient ApiClient { get; set; }
        public HttpClient HttpClient { get; set; }
        public CustomWebApplicationFactory<Program> WebApplicationFactory { get; set; }

        private readonly string _dbConnectionString;

        public BaseFixture()
        {
            
            Faker = new Faker("pt_BR");
            WebApplicationFactory = new CustomWebApplicationFactory<Program>();
            HttpClient = WebApplicationFactory.CreateClient();
            ApiClient = new ApiClient(HttpClient);
            var configuration = WebApplicationFactory.Services
            .GetService(typeof(IConfiguration));
            ArgumentNullException.ThrowIfNull(configuration);
            _dbConnectionString = ((IConfiguration)configuration)
                .GetConnectionString("AbreuBarberDb");
        }

        public AbreuBarberDbContext CreateDbContext()
        {
            ArgumentNullException.ThrowIfNull(_dbConnectionString);
            var context = new AbreuBarberDbContext(
                new DbContextOptionsBuilder<AbreuBarberDbContext>()
                .UseMySql(
                        _dbConnectionString,
                        ServerVersion.AutoDetect(_dbConnectionString)
                    )
                    .Options
            );
            return context;
        }

        public AbreuBarberDbContext CleanPersistence()
        {
            var context = CreateDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        protected Faker Faker { get; set; }
    }
}
