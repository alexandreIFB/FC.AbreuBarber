
using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using FC.AbreuBarber.EndToEndTests.Api.Procedure.Common;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.UpdateProcedure
{

    [CollectionDefinition(nameof(UpdateProcedureApiTestFixture))]
    public class UpdateProcedureApiTestFixtureCollection : ICollectionFixture<UpdateProcedureApiTestFixture> { }

    public class UpdateProcedureApiTestFixture : ProcedureBaseFixture
    {

        public UpdateProcedureInput GetUpdateProcedureInput(Guid id)
        {

            var input = new UpdateProcedureInput(
                id,
                GetValidProcedureName(),
                GetValidProcedureValue(),
                GetValidProcedureDescription(),
                GetRandomBoolean()
            );

            return input;
        }
    }
}
