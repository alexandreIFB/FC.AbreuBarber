
using Bogus;
using FC.AbreuBarber.UnitTests.Common;
using FC.AbreuBarber.UnitTests.Domain.Entity.Procedure;
using Xunit;

namespace FC.AbreuBarber.UnitTests.Domain.Entity.Procedure
{
    public class ProcedureTestFixture : BaseFixture
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

        public AbreuBarber.Domain.Entity.Procedure GetValidProcedure()
            => new(GetValidProcedureName(), GetValidProcedureDescription(), GetValidProcedureValue());
    }
}

[CollectionDefinition(nameof(ProcedureTestFixture))]
public class ProcedureTestFixtureCollection : ICollectionFixture<ProcedureTestFixture> { }