using FC.AbreuBarber.UnitTests.Application.Procedure.Common;
using FC.AbreuBarber.UnitTests.Application.Procedure.DeleteProcedure;
using Xunit;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.DeleteProcedure
{
    public class DeleteProcedureTestFixture : ProcedureUseCaseFixture
    {
    }
}

[CollectionDefinition(nameof(DeleteProcedureTestFixture))]
public class DeleteProcedureTestFixtureCollection : ICollectionFixture<DeleteProcedureTestFixture> { }