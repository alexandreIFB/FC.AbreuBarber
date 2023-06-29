using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure
{

    [Collection(nameof(UpdateProcedureTestFixture))]
    public class UpdateProcedureTest
    {
        private readonly UpdateProcedureTestFixture _fixture;
        public UpdateProcedureTest(UpdateProcedureTestFixture updateProcedureTestFixture) => _fixture = updateProcedureTestFixture;

        [Fact(DisplayName = nameof(UpdateProcedure))]
        [Trait("Application", "UpdateProcedure - Use Cases")]
        public async void UpdateProcedure()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var exampleProcedure = _fixture.GetValidProcedure();

            repositoryMock.Setup(x => x.Get(
                exampleProcedure.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleProcedure);

            var updateUseCase = new UseCase.UpdateProcedure(repositoryMock.Object, unitOfWorkMock.Object);
            var input = new UpdateProcedureInput(
                _fixture.GetValidProcedureName(),
                _fixture.GetValidProcedureDescription(),
                _fixture.GetValidProcedureValue(),
                !exampleProcedure.IsActive
            );

            ProcedureModelOutput output = await updateUseCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Value.Should().Be(input.Value);
            output.IsActive.Should().Be(input.IsActive);

            repositoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);

            repositoryMock.Verify(x => x.Update(
                exampleProcedure,
                It.IsAny<CancellationToken>()
            ), Times.Once);

            unitOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()
                ), Times.Once);
        }
    }
}
