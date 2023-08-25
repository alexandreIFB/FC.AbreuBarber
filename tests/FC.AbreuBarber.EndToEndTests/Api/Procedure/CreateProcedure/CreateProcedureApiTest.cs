

using Fc.AbreuBarber.Api.ApiModels.Response;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.CreateProcedure
{
    [Collection(nameof(CreateProcedureApiTestFixture))]
    public class CreateProcedureApiTest : IDisposable
    {
        private readonly CreateProcedureApiTestFixture _fixture;

        public CreateProcedureApiTest(CreateProcedureApiTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateProcedure))]
        [Trait("End2End/API", "Procedure/Create - Endpoints")]
        public async Task CreateProcedure()
        {
            var input = _fixture.GetValidInput();

            var (response, output) = await _fixture.
                ApiClient.Post<ApiResponse<ProcedureModelOutput>>(
                "/procedures",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.Created);
            output.Should().NotBeNull();
            output!.Data.Should().NotBeNull();
            output.Data.Id.Should().NotBeEmpty();
            output.Data.Name.Should().Be(input.Name);
            output.Data.Description.Should().Be(input.Description);
            output.Data.IsActive.Should().Be(input.IsActive);
            output.Data.Value.Should().Be(input.Value);
            output.Data.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

            var dbProcedure = await _fixture.Persistence.GetById(output.Data.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Id.Should().NotBeEmpty();
            dbProcedure.Name.Should().Be(output.Data.Name);
            dbProcedure.Description.Should().Be(output.Data.Description);
            dbProcedure.IsActive.Should().Be(output.Data.IsActive);
            dbProcedure.Value.Should().Be(output.Data.Value);
            dbProcedure.CreatedAt.Should().BeSameDateAs(output.Data.CreatedAt);
        }

        

        [Fact(DisplayName = nameof(ThrowWhenCantInstantiateAggregate))]
        [Trait("End2End/API", "Procedure/Create - Endpoints")]
        public async Task ThrowWhenCantInstantiateAggregate()
        {
            var input = _fixture.GetValidInput();

            input.Value = 25.21; 

            var (response, output) = await _fixture.
                ApiClient.Post<ProblemDetails>(
                "/procedures",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            output.Should().NotBeNull();
            output!.Title.Should().Be("One or more validation errors ocurred");
            output.Type.Should().Be("UnprocessableEntity");
            output.Status.Should().Be(StatusCodes.Status422UnprocessableEntity);
            output.Detail.Should().Be("Value should not be less than 30");
        }

        public void Dispose()
        {
            _fixture.CleanPersistence();
        }
    }
}
