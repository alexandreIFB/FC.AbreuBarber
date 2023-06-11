
using FC.SaudeAbreuCatalog.UnitTests.Domain.Entity.Procedure;
using Xunit;
using DomainEntity = FC.SaudeAbreuCatalgog.Domain.Entity;


namespace FC.SaudeAbreuCatalog.UnitTests.Domain.Entity.Procedure
{
    public class ProcedureTestFixture
    {
        public DomainEntity.Procedure GetValidProcedure() 
            => new ("Procedure Name", "Procedure Description", 40.50);
    }
}

[CollectionDefinition(nameof(ProcedureTestFixture))]
public class ProcedureTestFixtureCollection : ICollectionFixture<ProcedureTestFixture> { }