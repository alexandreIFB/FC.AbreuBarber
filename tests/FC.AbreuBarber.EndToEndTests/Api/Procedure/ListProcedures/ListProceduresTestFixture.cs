

using FC.AbreuBarber.EndToEndTests.Api.Procedure.Common;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.ListProcedures
{

    [CollectionDefinition(nameof(ListProceduresTestFixture))]
    public class ListProceduresTestFixtureCollection : ICollectionFixture<ListProceduresTestFixture> { }

    public class ListProceduresTestFixture : ProcedureBaseFixture
    {
    }
}
