using FC.SaudeAbreuCatalog.UnitTests.Application.Common;
using FC.SaudeAbreuCatalog.UnitTests.Application.Procedure.GetProcedure;
using Xunit;

namespace FC.SaudeAbreuCatalog.UnitTests.Application.Procedure.GetProcedure
{
    public class GetProcedureTestFixture : ProcedureUseCaseFixture
    {
    }
}


[CollectionDefinition(nameof(GetProcedureTestFixture))]
public class GetProcedureTestFixtureCollection : ICollectionFixture<GetProcedureTestFixture> { }
