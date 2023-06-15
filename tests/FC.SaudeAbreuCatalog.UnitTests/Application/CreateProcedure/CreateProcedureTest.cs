using FC.SaudeAbreuCatalgog.Domain.Entity;
using Moq;
using Xunit;
using UseCases = FC.SaudeAbreuCatalog.Application.UseCases.CreateProcedure;

namespace FC.SaudeAbreuCatalog.UnitTests.Application.CreateProcedure
{
    public class CreateProcedureTest
    {
        [Fact(DisplayName = nameof(CreateProcedure))]
        [Trait("Application", "CreateProcedure - Use Cases")]
        public async void CreateProcedure()
        {
            var repositoryMock = new Mock<IProcedureRepository>;
            var unitOfWorkMock = new Mock<IUnitOfWork>;

            var createUseCase = new UseCases.CreateProcedure(repositoryMock.Object, unitOfWorkMock.Object);

            var input = new CreateProcedureInput("Procedure Name", "Procedure Description", 40.50, true);

            var output = await createUseCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(repository => repository.Create(
                    It.IsAny<Procedure>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once());
            unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once());

            output.ShouldNotBeNull();
            output.Name.ShouldEqual("Procedure Name");
            output.Description.ShouldEqual("Procedure Description");
            output.Value.ShouldEqual(40.50);
            output.IsActive.ShouldEqual(true);
            (output.Id != null && output.Id != Guid.Empty).ShouldEqual(true);
            (output.CreatedAt != null && output.CreatedAt != default(DateTime)).ShouldEqual(true);
        }
    }
}
