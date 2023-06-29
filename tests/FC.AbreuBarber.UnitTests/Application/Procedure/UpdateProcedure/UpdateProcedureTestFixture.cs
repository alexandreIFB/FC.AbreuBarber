using FC.AbreuBarber.UnitTests.Application.Procedure.Common;
using FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure;
using Xunit;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure
{
    public class UpdateProcedureTestFixture : ProcedureUseCaseFixture
    {
    }
}

[CollectionDefinition(nameof(UpdateProcedureTestFixture))]
public class UpdateProcedureTestFixtureCollection : ICollectionFixture<UpdateProcedureTestFixture> { }