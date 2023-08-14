

using Bogus;
using Fc.AbreuBarber.Api;
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FC.AbreuBarber.EndToEndTests.Base
{
    public class BaseFixture
    {
        public ApiClient ApiClient { get; set; }
        public HttpClient HttpClient { get; set; }
        public CustomWebApplicationFactory<Program> WebApplicationFactory { get; set; }

        public BaseFixture()
        {
            
            Faker = new Faker("pt_BR");
            WebApplicationFactory = new CustomWebApplicationFactory<Program>();
            HttpClient = WebApplicationFactory.CreateClient();
            ApiClient = new ApiClient(HttpClient);
        }

        public AbreuBarberDbContext CreateDbContext()
        {
            var context = new AbreuBarberDbContext(
                new DbContextOptionsBuilder<AbreuBarberDbContext>()
                .UseInMemoryDatabase("end2end-tests-db")
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
