
using Fc.AbreuBarber.Api.ApiModels.Procedure;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            var input = _fixture.GetUpdateProcedureInput();


            var (response, output) = await _fixture.
                ApiClient.Put<ProcedureModelOutput>(
                $"/procedures/{procedureForUpdate.Id}",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Id.Should().Be(procedureForUpdate.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Value.Should().Be(input.Value);
            output.IsActive.Should().Be((bool)input.IsActive!);

            var dbProcedure = await _fixture.Persistence.GetById(output.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Name.Should().Be(input.Name);
            dbProcedure.Description.Should().Be(input.Description);
            dbProcedure.IsActive.Should().Be((bool)input.IsActive);
            dbProcedure.Value.Should().Be(input.Value);
        }

        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("End2End/API", "Procedure/Update - Endpoints")]
        public async Task UpdateOnlyName()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var procedureForUpdate = exampleProceduresList[10];

            var input = new UpdateProcedureApiInput(_fixture.GetValidProcedureName());


            var (response, output) = await _fixture.
                ApiClient.Put<ProcedureModelOutput>(
                $"/procedures/{procedureForUpdate.Id}",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Id.Should().Be(procedureForUpdate.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(procedureForUpdate.Description);
            output.Value.Should().Be(procedureForUpdate.Value);
            output.IsActive.Should().Be(procedureForUpdate.IsActive);

            var dbProcedure = await _fixture.Persistence.GetById(output.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Name.Should().Be(input.Name);
            dbProcedure.Description.Should().Be(procedureForUpdate.Description);
            dbProcedure.IsActive.Should().Be(procedureForUpdate.IsActive);
            dbProcedure.Value.Should().Be(procedureForUpdate.Value);
        }

        [Fact(DisplayName = nameof(UpdateNameAndValue))]
        [Trait("End2End/API", "Procedure/Update - Endpoints")]
        public async Task UpdateNameAndValue()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var procedureForUpdate = exampleProceduresList[10];

            var input = new UpdateProcedureApiInput(
                _fixture.GetValidProcedureName(),
                _fixture.GetValidProcedureValue()
            );


            var (response, output) = await _fixture.
                ApiClient.Put<ProcedureModelOutput>(
                $"/procedures/{procedureForUpdate.Id}",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Id.Should().Be(procedureForUpdate.Id);
            output.Name.Should().Be(input.Name);
            output.Value.Should().Be(input.Value);
            output.Description.Should().Be(procedureForUpdate.Description);
            output.IsActive.Should().Be(procedureForUpdate.IsActive);

            var dbProcedure = await _fixture.Persistence.GetById(output.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Name.Should().Be(input.Name);
            dbProcedure.Value.Should().Be(input.Value);
            dbProcedure.Description.Should().Be(procedureForUpdate.Description);
            dbProcedure.IsActive.Should().Be(procedureForUpdate.IsActive);
        }

        [Fact(DisplayName = nameof(ThrowWhenNotFound))]
        [Trait("End2End/API", "Procedure/Update - Endpoints")]
        public async Task ThrowWhenNotFound()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var randomGuid = Guid.NewGuid();

            var input = _fixture.GetUpdateProcedureInput();


            var (response, output) = await _fixture.
                ApiClient.Put<ProblemDetails>(
                $"/procedures/{randomGuid}",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
            output.Should().NotBeNull();
            output!.Title.Should().Be("Not Found");
            output.Status.Should().Be(StatusCodes.Status404NotFound);
            output.Type.Should().Be("NotFound");
            output.Detail.Should().Be($"Procedure '{randomGuid}' not found");
        }
    }
}
