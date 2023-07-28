
using FC.AbreuBarber.IntegrationTests.Base;
using Xunit;

namespace FC.AbreuBarber.IntegrationTests.Infra.Data.EF.Repositories.ProcedureRepository
{
    [CollectionDefinition(nameof(ProcedureRepositoryTestFixture))]
    public class ProcedureRepositoryTestFixtureCollection : ICollectionFixture<ProcedureRepositoryTestFixture> { }

    public class ProcedureRepositoryTestFixture : BaseFixture
    {
    }
}
