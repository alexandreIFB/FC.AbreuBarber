
using Bogus;
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FC.AbreuBarber.IntegrationTests.Base
{
    public class BaseFixture
    {
        public BaseFixture()
        {
            Faker = new Faker("pt_BR");
        }

        public AbreuBarberDbContext CreateDbContext(bool preservedData = false)
        {
            var context = new AbreuBarberDbContext(
                new DbContextOptionsBuilder<AbreuBarberDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
                );

            if (preservedData == false)
                context.Database.EnsureDeleted();

            return context;
        }

        protected Faker Faker { get; set; }
    }
}
