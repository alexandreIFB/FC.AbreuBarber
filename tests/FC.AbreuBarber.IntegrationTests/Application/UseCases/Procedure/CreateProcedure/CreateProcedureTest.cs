
using FC.AbreuBarber.Infra.Data.EF.Repositories;
using FluentAssertions;
using Xunit;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.Infra.Data.EF;
using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.CreateProcedure
{

    [Collection(nameof(CreateProcedureTestFixture))]
    public class CreateProcedureTest
    {
        private readonly CreateProcedureTestFixture _fixture;

        public CreateProcedureTest(CreateProcedureTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateProcedure))]
        [Trait("Integration/Application", "CreateProcedure - Use Cases")]
        public async void CreateProcedure()
        {
            var dbContext = _fixture.CreateDbContext();
            var repository = new ProcedureRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);
            var createUseCase = new UseCase.CreateProcedure(unitOfWork, repository);

            var input = _fixture.GetValidInput();

            var output = await createUseCase.Handle(input, CancellationToken.None);

            var dbProcedure = await _fixture.CreateDbContext(true).Procedures.FindAsync(output.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Name.Should().Be(input.Name);
            dbProcedure.Description.Should().Be(input.Description);
            dbProcedure.Value.Should().Be(input.Value);
            dbProcedure.IsActive.Should().Be(input.IsActive);
            dbProcedure.CreatedAt.Should().Be(output.CreatedAt);

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
            parameters: 10,
            MemberType = typeof(CreateProcedureTestDataGenerator)
        )]
        public async void ThrowWhenCantIntantiate(CreateProcedureInput invalidInput, string expectionMessage)
        {
            var dbContext = _fixture.CreateDbContext();
            var repository = new ProcedureRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);

            var createUseCase = new UseCase.CreateProcedure(
                unitOfWork,
                repository
            );

            Func<Task> task = async () => await createUseCase.Handle(invalidInput, CancellationToken.None);

            await task.Should().ThrowAsync<EntityValidationException>().WithMessage(expectionMessage);

            var dbCategoriesList = _fixture.CreateDbContext(true)
                .Procedures.AsNoTracking().ToList();
            dbCategoriesList.Should().HaveCount(0);
        }
    }
}
