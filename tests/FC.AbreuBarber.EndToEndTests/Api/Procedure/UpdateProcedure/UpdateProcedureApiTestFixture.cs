
using Fc.AbreuBarber.Api.ApiModels.Procedure;
using FC.AbreuBarber.EndToEndTests.Api.Procedure.Common;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.UpdateProcedure
{

    [CollectionDefinition(nameof(UpdateProcedureApiTestFixture))]
    public class UpdateProcedureApiTestFixtureCollection : ICollectionFixture<UpdateProcedureApiTestFixture> { }

    public class UpdateProcedureApiTestFixture : ProcedureBaseFixture
    {

        public UpdateProcedureApiInput GetUpdateProcedureInput()
        {

            var input = new UpdateProcedureApiInput(
                GetValidProcedureName(),
                GetValidProcedureValue(),
                GetValidProcedureDescription(),
                GetRandomBoolean()
            );

            return input;
        }
    }
}
