using Moq;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure;
using Xunit;
using FluentAssertions;
using FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure;
using FC.AbreuBarber.Application.Exceptions;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.GetProcedure
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
            var exampleProcedure = _fixture.GetValidProcedure();

            var getUseCase = new UseCase.GetProcedure(repositoryMock.Object);
            repositoryMock.Setup(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(exampleProcedure);

            var input = new GetProcedureInput(exampleProcedure.Id);


            var output = await getUseCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(exampleProcedure.Name);
            output.Description.Should().Be(exampleProcedure.Description);
            output.Value.Should().Be(exampleProcedure.Value);
            output.IsActive.Should().Be(exampleProcedure.IsActive);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default);

        }


        [Fact(DisplayName = nameof(NotFoundExceptionProcedure))]
        [Trait("Application", "GetProcedure - Use Cases")]
        public async void NotFoundExceptionProcedure()
        {
            var repositoryMock = _fixture.GetRepositoryMock();

            var exampleGuid = Guid.NewGuid();

            var getUseCase = new UseCase.GetProcedure(repositoryMock.Object);
            repositoryMock.Setup(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(new NotFoundException($"Procedure '{exampleGuid}' Not Found"));

            var input = new GetProcedureInput(exampleGuid);

            Func<Task> task = async () => await getUseCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();
        }
    }
}
