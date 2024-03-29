﻿
using Fc.AbreuBarber.Api.ApiModels.Procedure;
using Fc.AbreuBarber.Api.ApiModels.Response;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.UpdateProcedure
{

    [Collection(nameof(UpdateProcedureApiTestFixture))]
    public class UpdateProcedureApiTest : IDisposable
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
                ApiClient.Put<ApiResponse<ProcedureModelOutput>>(
                $"/procedures/{procedureForUpdate.Id}",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Data.Should().NotBeNull();
            output.Data.Id.Should().Be(procedureForUpdate.Id);
            output.Data.Name.Should().Be(input.Name);
            output.Data.Description.Should().Be(input.Description);
            output.Data.Value.Should().Be(input.Value);
            output.Data.IsActive.Should().Be((bool)input.IsActive!);

            var dbProcedure = await _fixture.Persistence.GetById(output.Data.Id);

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
                ApiClient.Put<ApiResponse<ProcedureModelOutput>>(
                $"/procedures/{procedureForUpdate.Id}",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Data.Id.Should().Be(procedureForUpdate.Id);
            output.Data.Name.Should().Be(input.Name);
            output.Data.Description.Should().Be(procedureForUpdate.Description);
            output.Data.Value.Should().Be(procedureForUpdate.Value);
            output.Data.IsActive.Should().Be(procedureForUpdate.IsActive);

            var dbProcedure = await _fixture.Persistence.GetById(output.Data.Id);

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
                ApiClient.Put<ApiResponse<ProcedureModelOutput>>(
                $"/procedures/{procedureForUpdate.Id}",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Data.Should().NotBeNull();
            output.Data.Id.Should().Be(procedureForUpdate.Id);
            output.Data.Name.Should().Be(input.Name);
            output.Data.Value.Should().Be(input.Value);
            output.Data.Description.Should().Be(procedureForUpdate.Description);
            output.Data.IsActive.Should().Be(procedureForUpdate.IsActive);

            var dbProcedure = await _fixture.Persistence.GetById(output.Data.Id);

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

        [Theory(DisplayName = nameof(ErrorWhenCantInstantiateAggregate))]
        [Trait("End2End/API", "Procedure/Update - Endpoints")]
        [MemberData(
            nameof(UpdateProcedureApiTestDataGenerator.GetInvalidInputs),
            parameters: 15,
            MemberType = typeof(UpdateProcedureApiTestDataGenerator))]
        public async void ErrorWhenCantInstantiateAggregate(
            UpdateProcedureApiInput input,
            string expectedDetail
        )
        {
            var exampleProceduresList = _fixture.GetExampleProceduresList(30);
            await _fixture.Persistence.InsertList(exampleProceduresList);
            var exampleProcedure = exampleProceduresList[10];

            var (response, output) = await _fixture.ApiClient.Put<ProblemDetails>(
                $"/procedures/{exampleProcedure.Id}",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            output.Should().NotBeNull();
            output!.Title.Should().Be("One or more validation errors ocurred");
            output.Type.Should().Be("UnprocessableEntity");
            output.Status.Should().Be(StatusCodes.Status422UnprocessableEntity);
            output.Detail.Should().Be(expectedDetail);
        }

        public void Dispose()
        {
            _fixture.CleanPersistence();
        }
    }
}
