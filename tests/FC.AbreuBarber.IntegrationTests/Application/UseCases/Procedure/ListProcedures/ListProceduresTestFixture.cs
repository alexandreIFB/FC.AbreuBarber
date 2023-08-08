using FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.Common;
using Xunit;


namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.ListProcedures
{
    [CollectionDefinition(nameof(ListProceduresTestFixture))]
    public class ListProceduresTestFixtureCollection : ICollectionFixture<ListProceduresTestFixture> { }

    public class ListProceduresTestFixture : ProcedureUseCaseFixture
    {
    }
}
