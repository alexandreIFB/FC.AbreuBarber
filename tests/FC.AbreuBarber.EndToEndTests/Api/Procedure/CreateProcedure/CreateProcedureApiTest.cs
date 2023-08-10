

using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.CreateProcedure
{
    [Collection(nameof(CreateProcedureApiTestFixture))]
    public class CreateProcedureApiTest
    {
        private readonly CreateProcedureApiTestFixture _fixture;

        public CreateProcedureApiTest(CreateProcedureApiTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateProcedure))]
        [Trait("End2End/API", "Procedure - Endpoints")]
        public async Task CreateProcedure()
        {
            var input = _fixture.GetValidInput();

            var (response,output) = await _fixture.ApiClient.Post<ProcedureModelOutput>(
                "/procedures",
                input
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

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
            dbProcedure.CreatedAt.Should().NotBeSameDateAs(output.CreatedAt);
        }

    }
}
