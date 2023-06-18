using FC.SaudeAbreuCatalgog.Application.Interfaces;
using FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure;
using FC.SaudeAbreuCatalgog.Domain.Entity;
using FC.SaudeAbreuCatalgog.Domain.Repository;
using FluentAssertions;
using Moq;
using Xunit;
using UseCases = FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure;

namespace FC.SaudeAbreuCatalog.UnitTests.Application.CreateProcedure
{
    public class CreateProcedureTest
    {
        [Fact(DisplayName = nameof(CreateProcedure))]
        [Trait("Application", "CreateProcedure - Use Cases")]
        public async void CreateProcedure()
        {
            var repositoryMock = new Mock<IProcedureRepository>();
            var unitOfWorkMock = new Mock<IUnityOfWork>();

            var createUseCase = new UseCases.CreateProcedure(unitOfWorkMock.Object , repositoryMock.Object);

            var input = new CreateProcedureInput("Procedure Name", 40.50, "Procedure Description",  true);

            var output = await createUseCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(repository => repository.Insert(
                    It.IsAny<Procedure>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once());
            unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once());

            output.Should().NotBeNull();
            output.Name.Should().Be("Procedure Name");
            output.Description.Should().Be("Procedure Description");
            output.Value.Should().Be(40.50);
            output.IsActive.Should().Be(true);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default);
        }


    }
}
