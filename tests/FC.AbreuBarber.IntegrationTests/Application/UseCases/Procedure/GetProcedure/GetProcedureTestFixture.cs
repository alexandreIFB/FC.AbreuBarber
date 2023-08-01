

using FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.Common;
using Xunit;

namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.GetProcedure
{
    [CollectionDefinition(nameof(GetProcedureTestFixture))]
    public class GetProcedureTestFixtureCollection : ICollectionFixture<GetProcedureTestFixture> { }


    public class GetProcedureTestFixture : ProcedureUseCaseFixture
    {

    }
}
