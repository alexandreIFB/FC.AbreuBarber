using FC.AbreuBarber.Application.Exceptions;
using FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure;


namespace FC.AbreuBarber.UnitTests.Application.Procedure.DeleteProcedure
{
    [Collection(nameof(DeleteProcedureTestFixture))]
    public class DeleteProcedureTest
    {
        private readonly DeleteProcedureTestFixture _fixture;
        public DeleteProcedureTest(DeleteProcedureTestFixture deleteProcedureTestFixture) => _fixture = deleteProcedureTestFixture;

        [Fact(DisplayName = nameof(DeleteProcedure))]
        [Trait("Application", "DeleteProcedure - Use Cases")]
        public async void DeleteProcedure()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var exampleProcedure = _fixture.GetValidProcedure();

            repositoryMock.Setup(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(exampleProcedure);

            var input = new DeleteProcedureInput(Guid.NewGuid());
            var deleteUseCase = new UseCase.DeleteProcedure(repositoryMock.Object, unitOfWorkMock.Object);
            await deleteUseCase.Handle(input,CancellationToken.None);


            repositoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);

            repositoryMock.Verify(x => x.Delete(
                exampleProcedure,
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Fact(DisplayName = nameof(DeleteProcedureErroWhenNotFound))]
        [Trait("Application", "DeleteProcedure - Use Cases")]
        public async void DeleteProcedureErroWhenNotFound()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var exampleGuid = Guid.NewGuid();

            repositoryMock.Setup(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(
            new NotFoundException($"Procedure '{exampleGuid}' not found")
            );


            var deleteUseCase = new UseCase.DeleteProcedure(repositoryMock.Object, unitOfWorkMock.Object);
            var input = new DeleteProcedureInput(exampleGuid);
            Func<Task> task = async () => await deleteUseCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}
