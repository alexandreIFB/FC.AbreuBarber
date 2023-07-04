using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using FC.AbreuBarber.Domain.Entity;
using FluentAssertions;
using Moq;
using Xunit;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using DomainEntity = FC.AbreuBarber.Domain.Entity;


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
    }
}
