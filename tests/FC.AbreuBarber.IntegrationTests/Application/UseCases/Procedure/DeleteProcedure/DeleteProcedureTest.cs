using FC.AbreuBarber.Application.Exceptions;
using FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure;
using FC.AbreuBarber.Infra.Data.EF;
using FC.AbreuBarber.Infra.Data.EF.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure;


namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.DeleteProcedure
{
    [Collection(nameof(DeleteProcedureTestFixture))]
    public class DeleteProcedureTest
    {

        private readonly DeleteProcedureTestFixture _fixture;

        public DeleteProcedureTest(DeleteProcedureTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(DeleteProcedure))]
        [Trait("Integration/Application", "DeleteProcedure - Use Cases")]
        public async void DeleteProcedure()
        {

            var exampleProcedure = _fixture.GetExampleProcedure();
            var exampleProcedures = _fixture.GetExampleProceduresList();
            var dbContext = _fixture.CreateDbContext();
            await dbContext.AddRangeAsync(exampleProcedures);
            var tracking = await dbContext.AddAsync(exampleProcedure);
            dbContext.SaveChanges();
            tracking.State = EntityState.Detached;
            var repository = new ProcedureRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);

            var input = new DeleteProcedureInput(exampleProcedure.Id);
            var deleteUseCase = new UseCase.DeleteProcedure(repository, unitOfWork);
            await deleteUseCase.Handle(input, CancellationToken.None);

            var dbProcedureDeleted = await _fixture.CreateDbContext(true).Procedures.FindAsync(exampleProcedure.Id);

            dbProcedureDeleted.Should().BeNull();

            var dbProcedures = await _fixture.CreateDbContext(true).Procedures.ToListAsync();

            dbProcedures.Should().HaveCount(exampleProcedures.Count);
        }

        [Fact(DisplayName = nameof(DeleteProcedureErroWhenNotFound))]
        [Trait("Integration/Application", "DeleteProcedure - Use Cases")]
        public async void DeleteProcedureErroWhenNotFound()
        {
            var exampleGuid = Guid.NewGuid();
            var input = new DeleteProcedureInput(exampleGuid);
            var dbContext = _fixture.CreateDbContext();
            await dbContext.AddRangeAsync(_fixture.GetExampleProceduresList());
            dbContext.SaveChanges();
            var repository = new ProcedureRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);

            var deleteUseCase = new UseCase.DeleteProcedure(repository, unitOfWork);
            Func<Task> task = async () => await deleteUseCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Procedure '{input.Id}' not found"); ;
        }

    }
}
