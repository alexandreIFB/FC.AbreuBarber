

using FC.AbreuBarber.EndToEndTests.Api.Procedure.Common;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.DeleteProcedure
{

    [CollectionDefinition(nameof(DeleteProcedureApiTestFixture))]
    public class DeleteProcedureApiTestFixtureCollection : ICollectionFixture<DeleteProcedureApiTestFixture> { }

    public class DeleteProcedureApiTestFixture : ProcedureBaseFixture
    {
    }
}
