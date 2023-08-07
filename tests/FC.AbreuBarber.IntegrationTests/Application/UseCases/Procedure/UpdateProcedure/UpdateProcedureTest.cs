
using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using DomainEntity = FC.AbreuBarber.Domain.Entity;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using FC.AbreuBarber.Infra.Data.EF;
using FC.AbreuBarber.Infra.Data.EF.Repositories;
using Xunit;
using FluentAssertions;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Domain.Exceptions;
using FC.AbreuBarber.Application.Exceptions;

namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.UpdateProcedure
{

    [Collection(nameof(UpdateProcedureTestFixture))]
    public class UpdateProcedureTest
    {

        private readonly UpdateProcedureTestFixture _fixture;

        public UpdateProcedureTest(UpdateProcedureTestFixture fixture)
            => _fixture = fixture;

        [Theory(DisplayName = nameof(UpdateProcedure))]
        [Trait("Integration/Application", "UpdateProcedure - Use Cases")]
        [MemberData(
            nameof(UpdateProcedureTestDataGenerator.GetProcedureToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateProcedureTestDataGenerator)
            )]
        public async void UpdateProcedure(DomainEntity.Procedure exampleProcedure, UpdateProcedureInput input)
        {

            var dbContext = _fixture.CreateDbContext();
            var trackingInfo = await dbContext.AddAsync(exampleProcedure);
            await dbContext.AddRangeAsync(_fixture.GetExampleProceduresList());
            await dbContext.SaveChangesAsync();
            trackingInfo.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            var repository = new ProcedureRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);
            var updateUseCase = new UseCase.UpdateProcedure(repository, unitOfWork);

            var output = await updateUseCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Value.Should().Be(input.Value);
            output.IsActive.Should().Be((bool)input.IsActive!);

            var dbProcedure = await _fixture.CreateDbContext(true).Procedures.FindAsync(output.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Name.Should().Be(input.Name);
            dbProcedure.Description.Should().Be(input.Description);
            dbProcedure.Value.Should().Be(input.Value);
            dbProcedure.IsActive.Should().Be((bool)input.IsActive);
            dbProcedure.CreatedAt.Should().Be(output.CreatedAt);
        }

        [Theory(DisplayName = nameof(UpdateProcedureWhithoutProvidingIsActive))]
        [Trait("Integration/Application", "UpdateProcedure - Use Cases")]
        [MemberData(
            nameof(UpdateProcedureTestDataGenerator.GetProcedureToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateProcedureTestDataGenerator)
            )]
        public async void UpdateProcedureWhithoutProvidingIsActive(
            DomainEntity.Procedure exampleProcedure,
            UpdateProcedureInput exampleInput)
        {
            var input = new UpdateProcedureInput(exampleInput.Id, exampleInput.Name, exampleInput.Value, exampleInput.Description);


            var dbContext = _fixture.CreateDbContext();
            await dbContext.AddRangeAsync(_fixture.GetExampleProceduresList());
            var trackingInfo = await dbContext.AddAsync(exampleProcedure);
            await dbContext.SaveChangesAsync();
            trackingInfo.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            var repository = new ProcedureRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);


            var updateUseCase = new UseCase.UpdateProcedure(repository, unitOfWork);

            var output = await updateUseCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Value.Should().Be(input.Value);
            output.IsActive.Should().Be((bool)exampleProcedure.IsActive!);


            var dbProcedure = await _fixture.CreateDbContext(true).Procedures.FindAsync(output.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Name.Should().Be(input.Name);
            dbProcedure.Description.Should().Be(input.Description);
            dbProcedure.Value.Should().Be(input.Value);
            dbProcedure.IsActive.Should().Be((bool)exampleProcedure.IsActive!);
            dbProcedure.CreatedAt.Should().Be(output.CreatedAt);

        }

        [Theory(DisplayName = nameof(UpdateProcedureOnlyName))]
        [Trait("Integration/Application", "UpdateProcedure - Use Cases")]
        [MemberData(
            nameof(UpdateProcedureTestDataGenerator.GetProcedureToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateProcedureTestDataGenerator)
            )]
        public async void UpdateProcedureOnlyName(
            DomainEntity.Procedure exampleProcedure,
            UpdateProcedureInput exampleInput)
        {
            var input = new UpdateProcedureInput(exampleInput.Id, exampleInput.Name);
            var dbContext = _fixture.CreateDbContext();
            await dbContext.AddRangeAsync(_fixture.GetExampleProceduresList());
            var trackingInfo = await dbContext.AddAsync(exampleProcedure);
            await dbContext.SaveChangesAsync();
            trackingInfo.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            var repository = new ProcedureRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);

            var updateUseCase = new UseCase.UpdateProcedure(repository, unitOfWork);

            var output = await updateUseCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(exampleProcedure.Description);
            output.Value.Should().Be(exampleProcedure.Value);
            output.IsActive.Should().Be((bool)exampleProcedure.IsActive!);


            var dbProcedure = await _fixture.CreateDbContext(true).Procedures.FindAsync(output.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Name.Should().Be(input.Name);
            dbProcedure.Description.Should().Be(exampleProcedure.Description);
            dbProcedure.Value.Should().Be(exampleProcedure.Value);
            dbProcedure.IsActive.Should().Be((bool)exampleProcedure.IsActive!);
            dbProcedure.CreatedAt.Should().Be(output.CreatedAt);
        }

        [Fact(DisplayName = nameof(ErrorWhenNotFoundProcedure))]
        [Trait("Integration/Application", "UpdateProcedure - Use Cases")]
        public async void ErrorWhenNotFoundProcedure()
        {
            var invalidInput = _fixture.GetValidProcedureInput();
            var dbContext = _fixture.CreateDbContext();
            await dbContext.AddRangeAsync(_fixture.GetExampleProceduresList());
            dbContext.SaveChanges();
            var repository = new ProcedureRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);

            var updateUseCase = new UseCase.UpdateProcedure(repository, unitOfWork);

            Func<Task> task = async () => await updateUseCase.Handle(invalidInput, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>().WithMessage($"Procedure '{invalidInput.Id}' not found");
        }

        [Theory(DisplayName = nameof(ThrowWhenCantUpdate))]
        [Trait("Integration/Application", "UpdateProcedure - Use Cases")]
        [MemberData(
            nameof(UpdateProcedureTestDataGenerator.GetInvalidInputs),
            parameters: 15,
            MemberType = typeof(UpdateProcedureTestDataGenerator)
        )]
        public async void ThrowWhenCantUpdate(UpdateProcedureInput invalidInput, string expectionMessage)
        {
            var dbContext = _fixture.CreateDbContext();
            var exampleProcedures = _fixture.GetExampleProceduresList();
            invalidInput.Id = exampleProcedures[0].Id;
            await dbContext.AddRangeAsync(exampleProcedures);
            dbContext.SaveChanges();
            var repository = new ProcedureRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);

            var updateUseCase = new UseCase.UpdateProcedure(repository, unitOfWork);

            Func<Task> task = async () => await updateUseCase.Handle(invalidInput, CancellationToken.None);

            await task.Should().ThrowAsync<EntityValidationException>().WithMessage(expectionMessage);
        }

    }
}
