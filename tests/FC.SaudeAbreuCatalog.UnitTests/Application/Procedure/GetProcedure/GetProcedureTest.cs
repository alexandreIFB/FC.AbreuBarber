using FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.GetProcedure;
using Moq;
using UseCase = FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.GetProcedure;
using Xunit;
using FluentAssertions;

namespace FC.SaudeAbreuCatalog.UnitTests.Application.Procedure.GetProcedure
{
    [Collection(nameof(GetProcedureTestFixture))]
    public class GetProcedureTest
    {
        private readonly GetProcedureTestFixture _fixture;

        public GetProcedureTest(GetProcedureTestFixture fixture) =>
            _fixture = fixture;

        [Fact(DisplayName = nameof(GetProcedure))]
        [Trait("Application", "GetProcedure - Use Cases")]
        public async Task GetProcedure()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var getUseCase = new UseCase.GetProcedure(unitOfWorkMock.Object, repositoryMock.Object);


            var exampleProcedure = _fixture.GetValidProcedure();

            repositoryMock.Setup(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(exampleProcedure);

            var input = new GetProcedureInput(exampleProcedure.Id);


            var procedure = await getUseCase.Handle(input, CancellationToken.None);

            procedure.Id.Should().Be(exampleProcedure.Id);

        }
            

    }
}
