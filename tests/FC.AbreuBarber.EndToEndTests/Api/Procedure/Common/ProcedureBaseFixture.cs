using DomainEntity = FC.AbreuBarber.Domain.Entity;
using FC.AbreuBarber.EndToEndTests.Base;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.Common
{
    public class ProcedureBaseFixture : BaseFixture
    {

        public ProcedurePersistence Persistence;

        public ProcedureBaseFixture() : base()
        {
            Persistence = new ProcedurePersistence(CreateDbContext());
        }

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
    }
}
