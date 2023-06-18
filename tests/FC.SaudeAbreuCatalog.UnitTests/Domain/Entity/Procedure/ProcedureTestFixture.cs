
using Bogus;
using FC.SaudeAbreuCatalog.UnitTests.Common;
using FC.SaudeAbreuCatalog.UnitTests.Domain.Entity.Procedure;
using Xunit;
using DomainEntity = FC.SaudeAbreuCatalgog.Domain.Entity;



namespace FC.SaudeAbreuCatalog.UnitTests.Domain.Entity.Procedure
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

        public Double GetValidProcedureValue()
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

        public DomainEntity.Procedure GetValidProcedure() 
            => new(GetValidProcedureName(), GetValidProcedureDescription(), GetValidProcedureValue() );
    }
}

[CollectionDefinition(nameof(ProcedureTestFixture))]
public class ProcedureTestFixtureCollection : ICollectionFixture<ProcedureTestFixture> { }