
using DomainEntity = FC.AbreuBarber.Domain.Entity;
using FC.AbreuBarber.IntegrationTests.Base;
using FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;

namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.Common
{
    public class ProcedureUseCaseFixture : BaseFixture
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

        public bool GetRandomBoolean()
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

        public DomainEntity.Procedure GetExampleProcedure()
        {
            return new(GetValidProcedureName(), GetValidProcedureDescription(), GetValidProcedureValue(), GetRandomBoolean());
        }

        public List<DomainEntity.Procedure> GetExampleProceduresList(int length = 15)
        {
            return Enumerable.Range(1, length).Select(_ =>
                GetExampleProcedure()
            ).ToList();
        }

        public ListProceduresInput GetListProceduresInput()
        {

            var random = new Random();

            return new ListProceduresInput(
                page: random.Next(1, 20),
                perPage: random.Next(15, 40),
                search: Faker.Commerce.ProductName(),
                sort: "name",
                dir: random.Next(0, 10) > 5 ? SearchOrder.Asc : SearchOrder.Desc
            );
        }
    }
}
