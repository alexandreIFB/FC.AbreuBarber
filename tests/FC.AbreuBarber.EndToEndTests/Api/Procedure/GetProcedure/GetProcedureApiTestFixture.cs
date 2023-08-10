

using FC.AbreuBarber.EndToEndTests.Api.Procedure.Common;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.GetProcedure
{
    [CollectionDefinition(nameof(GetProcedureApiTestFixture))]
    public class GetProcedureApiTestFixtureCollection 
        : ICollectionFixture<GetProcedureApiTestFixture> { }

    public class GetProcedureApiTestFixture : ProcedureBaseFixture
    { }
}
