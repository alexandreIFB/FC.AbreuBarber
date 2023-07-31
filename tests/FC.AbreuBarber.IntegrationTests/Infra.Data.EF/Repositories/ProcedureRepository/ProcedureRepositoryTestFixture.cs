
using FC.AbreuBarber.Domain.Entity;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using FC.AbreuBarber.Infra.Data.EF.Configurations;
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

        public List<Procedure> GetExampleProceduresList(int length = 15)
        {
            return Enumerable.Range(1, length).Select(_ => 
                GetExampleProcedure()
            ).ToList();
        }

        public List<Procedure> GetExampleProceduresListWithNames(List<string> names)
        {
            return names.Select(name => {
                var procedure = GetExampleProcedure();
                procedure.Update(name);
                return procedure;
            }).ToList();
        }

        public List<Procedure> CloneProceduresListOrdered(List<Procedure> proceduresList ,string orderBy, SearchOrder order)
        {
            var listClone = new List<Procedure>(proceduresList);
            var orderedEnumerable = (orderBy, order) switch
            {
                ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name),
                ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name),
                ("value", SearchOrder.Asc) => listClone.OrderBy(x => x.Value),
                ("value", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Value),
                ("createdAt", SearchOrder.Asc) => listClone.OrderBy(x => x.CreatedAt),
                ("createdAt", SearchOrder.Desc) => listClone.OrderByDescending(x => x.CreatedAt),
                _ => listClone.OrderBy(x => x.Name)
            };

            return orderedEnumerable.ToList();
        }


        public AbreuBarberDbContext CreateDbContext(bool preservedData = false)
        {
            var context = new AbreuBarberDbContext(
                new DbContextOptionsBuilder<AbreuBarberDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
                );

            if(preservedData == false)
                context.Database.EnsureDeleted();

            return context;
        }



    }
}
