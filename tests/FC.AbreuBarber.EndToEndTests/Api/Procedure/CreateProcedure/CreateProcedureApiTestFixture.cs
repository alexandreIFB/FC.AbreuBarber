

using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.EndToEndTests.Api.Procedure.Common;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.CreateProcedure
{

    [CollectionDefinition(nameof(CreateProcedureApiTestFixture))]
    public class CreateProcedureApiTestFixtureCollection : ICollectionFixture<CreateProcedureApiTestFixture> { }
    public class CreateProcedureApiTestFixture : ProcedureBaseFixture
    {



        public CreateProcedureInput GetValidInput()
        {
            return new CreateProcedureInput(
                GetValidProcedureName(),
                GetValidProcedureValue(), 
                GetValidProcedureDescription(),
                GetRandomBoolean()
               );
        }
    }
}
