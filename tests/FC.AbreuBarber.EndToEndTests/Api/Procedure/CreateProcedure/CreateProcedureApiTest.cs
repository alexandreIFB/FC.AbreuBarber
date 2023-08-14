

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
                ApiClient.Post<ProcedureModelOutput>(
                "/procedures",
                input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.Created);
            output.Should().NotBeNull();
            output!.Id.Should().NotBeEmpty();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive);
            output.Value.Should().Be(input.Value);
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

            var dbProcedure = await _fixture.Persistence.GetById(output.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Id.Should().NotBeEmpty();
            dbProcedure.Name.Should().Be(output.Name);
            dbProcedure.Description.Should().Be(output.Description);
            dbProcedure.IsActive.Should().Be(output.IsActive);
            dbProcedure.Value.Should().Be(output.Value);
            dbProcedure.CreatedAt.Should().BeSameDateAs(output.CreatedAt);
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
