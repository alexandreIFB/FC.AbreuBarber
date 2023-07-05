using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using FC.AbreuBarber.UnitTests.Application.Procedure.Common;
using FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure;
using Xunit;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure
{
    public class UpdateProcedureTestFixture : ProcedureUseCaseFixture
    {

        public UpdateProcedureInput GetValidInput(Guid? id = null)
        {
            var fixture = new UpdateProcedureTestFixture();

            var input = new UpdateProcedureInput(
                    id ?? Guid.NewGuid(),
                    fixture.GetValidProcedureName(),
                    fixture.GetValidProcedureValue(),
                    fixture.GetValidProcedureDescription(),
                    fixture.GetRandomBoolean()
                   );

            return input;
        }
    }
}

[CollectionDefinition(nameof(UpdateProcedureTestFixture))]
public class UpdateProcedureTestFixtureCollection : ICollectionFixture<UpdateProcedureTestFixture> { }