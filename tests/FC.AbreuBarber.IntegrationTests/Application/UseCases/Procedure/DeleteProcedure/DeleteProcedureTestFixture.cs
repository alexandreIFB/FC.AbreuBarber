
using FC.AbreuBarber.IntegrationTests.Infra.Data.EF.Repositories.ProcedureRepository;
using Xunit;

namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.DeleteProcedure
{

    [CollectionDefinition(nameof(DeleteProcedureTestFixture))]
    public class DeleteProcedureTestFixtureCollection : ICollectionFixture<DeleteProcedureTestFixture> { }

    public class DeleteProcedureTestFixture : ProcedureRepositoryTestFixture
    {
    }
}
