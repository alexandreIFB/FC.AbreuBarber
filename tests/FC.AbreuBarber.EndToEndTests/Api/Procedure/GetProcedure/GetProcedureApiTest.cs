using Fc.AbreuBarber.Api.ApiModels.Response;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.GetProcedure
{
    [Collection(nameof(GetProcedureApiTestFixture))]
    public class GetProcedureApiTest : IDisposable
    {

        private readonly GetProcedureApiTestFixture _fixture;

        public GetProcedureApiTest(GetProcedureApiTestFixture fixture)
            => _fixture = fixture;


        [Fact(DisplayName = nameof(GetById))]
        [Trait("End2End/API", "Procedure/Get - Endpoints")]
        public async Task GetById()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var expectedGetProcedure = exampleProceduresList[10];

            var (response, output) = await _fixture.
                ApiClient.Get<ApiResponse<ProcedureModelOutput>>(
                $"/procedures/{expectedGetProcedure.Id}"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Data.Should().NotBeNull();
            output.Data.Id.Should().NotBeEmpty();
            output.Data.Name.Should().Be(expectedGetProcedure.Name);
            output.Data.Description.Should().Be(expectedGetProcedure.Description);
            output.Data.IsActive.Should().Be(expectedGetProcedure.IsActive);
            output.Data.Value.Should().Be(expectedGetProcedure.Value);
            output.Data.CreatedAt.Should().BeSameDateAs(expectedGetProcedure.CreatedAt);
        }

        [Fact(DisplayName = nameof(ThrowWhenNotFound))]
        [Trait("End2End/API", "Procedure/Get - Endpoints")]
        public async Task ThrowWhenNotFound()
        {

            var exampleProceduresList = _fixture.GetExampleProceduresList(15);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var randomGuid = Guid.NewGuid();

            var (response, output) = await _fixture.
                ApiClient.Get<ProblemDetails>(
                $"/procedures/{randomGuid}"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
            output.Should().NotBeNull();
            output!.Title.Should().Be("Not Found");
            output.Status.Should().Be(StatusCodes.Status404NotFound);
            output.Type.Should().Be("NotFound");
            output.Detail.Should().Be($"Procedure '{randomGuid}' not found");
        }

        public void Dispose()
        {
            _fixture.CleanPersistence();
        }
    }
}
