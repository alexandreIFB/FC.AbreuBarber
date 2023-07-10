using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using FC.AbreuBarber.Domain.Entity;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using DomainEntity = FC.AbreuBarber.Domain.Entity;
using FC.AbreuBarber.Application.Exceptions;
using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.Domain.Exceptions;
using FC.AbreuBarber.UnitTests.Application.Procedure.CreateProcedure;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure
{

    [Collection(nameof(UpdateProcedureTestFixture))]
    public class UpdateProcedureTest
    {
        private readonly UpdateProcedureTestFixture _fixture;
        public UpdateProcedureTest(UpdateProcedureTestFixture updateProcedureTestFixture) => _fixture = updateProcedureTestFixture;

        [Theory(DisplayName = nameof(UpdateProcedure))]
        [Trait("Application", "UpdateProcedure - Use Cases")]
        [MemberData(
            nameof(UpdateProcedureTestDataGenerator.GetProcedureToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateProcedureTestDataGenerator)
            )]
        public async void UpdateProcedure(DomainEntity.Procedure exampleProcedure,UpdateProcedureInput input)
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            repositoryMock.Setup(x => x.Get(
                exampleProcedure.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleProcedure);

            var updateUseCase = new UseCase.UpdateProcedure(repositoryMock.Object, unitOfWorkMock.Object);
            

            ProcedureModelOutput output = await updateUseCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Value.Should().Be(input.Value);
            output.IsActive.Should().Be((bool)input.IsActive!);

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

        [Fact(DisplayName = nameof(ThrowWhenProcedureNotFound))]
        [Trait("Application", "UpdateProcedure - Use Cases")]
        public async void ThrowWhenProcedureNotFound()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var input = _fixture.GetValidInput();

            repositoryMock.Setup(x => x.Get(
                input.Id,
                It.IsAny<CancellationToken>())
            ).ThrowsAsync(new NotFoundException($"Procedure '{input.Id}' not found"));

            var updateUseCase = new UseCase.UpdateProcedure(repositoryMock.Object, unitOfWorkMock.Object);

            var task = async () => await updateUseCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Theory(DisplayName = nameof(UpdateProcedureWhithoutProvidingIsActive))]
        [Trait("Application", "UpdateProcedure - Use Cases")]
        [MemberData(
            nameof(UpdateProcedureTestDataGenerator.GetProcedureToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateProcedureTestDataGenerator)
            )]
        public async void UpdateProcedureWhithoutProvidingIsActive(
            DomainEntity.Procedure exampleProcedure,
            UpdateProcedureInput exampleInput)
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var input = new UpdateProcedureInput(exampleInput.Id, exampleInput.Name, exampleInput.Value,exampleInput.Description);

            repositoryMock.Setup(x => x.Get(
                exampleProcedure.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleProcedure);

            var updateUseCase = new UseCase.UpdateProcedure(repositoryMock.Object, unitOfWorkMock.Object);


            ProcedureModelOutput output = await updateUseCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Value.Should().Be(input.Value);
            output.IsActive.Should().Be((bool)exampleProcedure.IsActive!);

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


        [Theory(DisplayName = nameof(UpdateProcedureOnlyName))]
        [Trait("Application", "UpdateProcedure - Use Cases")]
        [MemberData(
            nameof(UpdateProcedureTestDataGenerator.GetProcedureToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateProcedureTestDataGenerator)
            )]
        public async void UpdateProcedureOnlyName(
            DomainEntity.Procedure exampleProcedure,
            UpdateProcedureInput exampleInput)
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var input = new UpdateProcedureInput(exampleInput.Id, exampleInput.Name);

            repositoryMock.Setup(x => x.Get(
                exampleProcedure.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleProcedure);

            var updateUseCase = new UseCase.UpdateProcedure(repositoryMock.Object, unitOfWorkMock.Object);


            ProcedureModelOutput output = await updateUseCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(exampleProcedure.Description);
            output.Value.Should().Be(exampleProcedure.Value);
            output.IsActive.Should().Be((bool)exampleProcedure.IsActive!);

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

        [Theory(DisplayName = nameof(ThrowWhenCantUpdate))]
        [Trait("Application", "UpdateProcedure - Use Cases")]
        [MemberData(
            nameof(UpdateProcedureTestDataGenerator.GetInvalidInputs),
            parameters: 15,
            MemberType = typeof(UpdateProcedureTestDataGenerator)
        )]
        public async void ThrowWhenCantUpdate(UpdateProcedureInput invalidInput, string expectionMessage)
        {
            var exampleProcedure = _fixture.GetValidProcedure();
            invalidInput.Id = exampleProcedure.Id;
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(
                exampleProcedure.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleProcedure);

            var updateUseCase = new UseCase.UpdateProcedure(repositoryMock.Object, unitOfWorkMock.Object);

            Func<Task> task = async () => await updateUseCase.Handle(invalidInput, CancellationToken.None);

            await task.Should().ThrowAsync<EntityValidationException>().WithMessage(expectionMessage);

            repositoryMock.Verify(x => x.Get(
                exampleProcedure.Id,
                It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
