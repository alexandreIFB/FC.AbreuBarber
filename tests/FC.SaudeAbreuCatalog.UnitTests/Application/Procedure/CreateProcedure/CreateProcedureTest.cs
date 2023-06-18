using FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure;
using FC.SaudeAbreuCatalgog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using UseCases = FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure;
using DomainEntity = FC.SaudeAbreuCatalgog.Domain.Entity;



namespace FC.SaudeAbreuCatalog.UnitTests.Application.Procedure.CreateProcedure
{

    [Collection(nameof(CreateProcedureTestFixture))]
    public class CreateProcedureTest
    {

        private readonly CreateProcedureTestFixture _fixture;

        public CreateProcedureTest(CreateProcedureTestFixture createProcedureTestFixture) => _fixture = createProcedureTestFixture;


        [Fact(DisplayName = nameof(ThrowWhenCantIntantiate))]
        [Trait("Application", "CreateProcedure - Use Cases")]
        public async void CreateProcedure()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var createUseCase = new UseCases.CreateProcedure(unitOfWorkMock.Object, repositoryMock.Object);

            var input = _fixture.GetValidInput();

            var output = await createUseCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(repository => repository.Insert(
                    It.IsAny<DomainEntity.Procedure>(),
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

        [Theory(DisplayName = nameof(ThrowWhenCantIntantiate))]
        [Trait("Application", "CreateProcedure - Use Cases")]
        [MemberData(
            nameof(CreateProcedureTestDataGenerator.GetInvalidInputs),
            parameters: 30,
            MemberType = typeof(CreateProcedureTestDataGenerator)
        )]
        public async void ThrowWhenCantIntantiate(CreateProcedureInput invalidInput, string expectionMessage)
        {
            var createUseCase = new UseCases.CreateProcedure(
                _fixture.GetUnitOfWorkMock().Object,
                _fixture.GetRepositoryMock().Object
            );

            Func<Task> task = async () => await createUseCase.Handle(invalidInput, CancellationToken.None);

            await task.Should().ThrowAsync<EntityValidationException>().WithMessage(expectionMessage);
        }
    }
}
