using FC.SaudeAbreuCatalgog.Application.Interfaces;
using FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure;
using FC.SaudeAbreuCatalgog.Domain.Entity;
using FC.SaudeAbreuCatalgog.Domain.Repository;
using FC.SaudeAbreuCatalog.UnitTests.Domain.Entity.Procedure;
using FluentAssertions;
using Moq;
using Xunit;
using UseCases = FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure;

namespace FC.SaudeAbreuCatalog.UnitTests.Application.CreateProcedure
{

    [Collection(nameof(CreateProcedureTestFixture))]
    public class CreateProcedureTest
    {

        private readonly CreateProcedureTestFixture _fixture;

        public CreateProcedureTest(CreateProcedureTestFixture createProcedureTestFixture) => _fixture = createProcedureTestFixture;


        [Fact(DisplayName = nameof(CreateProcedure))]
        [Trait("Application", "CreateProcedure - Use Cases")]
        public async void CreateProcedure()
        {
            var repositoryMock = new Mock<IProcedureRepository>();
            var unitOfWorkMock = new Mock<IUnityOfWork>();

            var createUseCase = new UseCases.CreateProcedure(unitOfWorkMock.Object , repositoryMock.Object);

            var input = _fixture.GetValidInput();

            var output = await createUseCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(repository => repository.Insert(
                    It.IsAny<Procedure>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once());
            unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once());

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Value.Should().Be(input.Value);
            output.IsActive.Should().Be(input.IsActive);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default);
        }


    }
}
