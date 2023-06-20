using FC.AbreuBarber.UnitTests.Application.Procedure.Common;
using FC.AbreuBarber.UnitTests.Application.Procedure.GetProcedure;
using Xunit;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.GetProcedure
{
    public class GetProcedureTestFixture : ProcedureUseCaseFixture
    {
    }
}


[CollectionDefinition(nameof(GetProcedureTestFixture))]
public class GetProcedureTestFixtureCollection : ICollectionFixture<GetProcedureTestFixture> { }
