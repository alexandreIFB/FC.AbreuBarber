
using FC.AbreuBarber.Domain.Entity;
using FC.AbreuBarber.IntegrationTests.Base;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FC.AbreuBarber.IntegrationTests.Infra.Data.EF.Repositories.ProcedureRepository
{
    [CollectionDefinition(nameof(ProcedureRepositoryTestFixture))]
    public class ProcedureRepositoryTestFixtureCollection : ICollectionFixture<ProcedureRepositoryTestFixture> { }

    public class ProcedureRepositoryTestFixture : BaseFixture
    {
        public string GetValidProcedureName()
        {
            var procedureName = "";
            while (procedureName.Length < 3)
                procedureName = GenerateProcedureName();
            if (procedureName.Length > 255)
                procedureName = procedureName[..255];
            return procedureName;
        }

        public string GetValidProcedureDescription()
        {
            var procedureDescription = GenerateProcedureDescription();
            if (procedureDescription.Length > 10_000)
                procedureDescription = procedureDescription[..10_000];
            return procedureDescription;
        }

        public double GetValidProcedureValue()
        {
            var procedureValue = Faker.Random.Double(30, 1000);

            return procedureValue;
        }

        public bool getRandomBoolean()
        => new Random().NextDouble() < 0.5;

        private string GenerateProcedureName()
        {
            var procedureName = Faker.Lorem.Sentence(2);
            return procedureName;
        }

        private string GenerateProcedureDescription()
        {
            var procedureDescription = Faker.Lorem.Paragraph();
            return procedureDescription;
        }

        public Procedure GetExampleProcedure()
        {
            return new(GetValidProcedureName(), GetValidProcedureDescription(), GetValidProcedureValue(), getRandomBoolean());
        }


        public AbreuBarberDbContext CreateDbContext()
        {
            var dbContext = new AbreuBarberDbContext(
                new DbContextOptionsBuilder<AbreuBarberDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
                );

            return dbContext;
        }
    }
}
