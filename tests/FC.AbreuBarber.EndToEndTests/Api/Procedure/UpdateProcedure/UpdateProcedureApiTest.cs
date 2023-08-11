
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
using System.Net;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.UpdateProcedure
{

    [Collection(nameof(UpdateProcedureApiTestFixture))]
    public class UpdateProcedureApiTest
    {

        private readonly UpdateProcedureApiTestFixture _fixture;

        public UpdateProcedureApiTest(UpdateProcedureApiTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(UpdateOk))]
        [Trait("End2End/API", "Procedure/Update - Endpoints")]
        public async Task UpdateOk()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var procedureForUpdate = exampleProceduresList[10];

            var input = _fixture.GetUpdateProcedureInput(procedureForUpdate.Id);


            var (response, output) = await _fixture.
                ApiClient.Put<ProcedureModelOutput>(
                $"/procedures/{procedureForUpdate.Id}",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Id.Should().Be(input.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be((bool)input.IsActive!);
            output.Value.Should().Be(input.Value);
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

            var dbProcedure = await _fixture.Persistence.GetById(output.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Id.Should().NotBeEmpty();
            dbProcedure.Name.Should().Be(output.Name);
            dbProcedure.Description.Should().Be(output.Description);
            dbProcedure.IsActive.Should().Be(output.IsActive);
            dbProcedure.Value.Should().Be(input.Value);
            dbProcedure.CreatedAt.Should().BeSameDateAs(output.CreatedAt);
        }
    }
}
