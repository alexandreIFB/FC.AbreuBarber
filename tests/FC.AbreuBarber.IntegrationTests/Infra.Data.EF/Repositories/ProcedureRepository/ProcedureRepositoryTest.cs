
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Repository = FC.AbreuBarber.Infra.Data.EF.Repositories;
using Xunit;
using FluentAssertions;
using FC.AbreuBarber.Application.Exceptions;

namespace FC.AbreuBarber.IntegrationTests.Infra.Data.EF.Repositories.ProcedureRepository
{


    [Collection(nameof(ProcedureRepositoryTestFixture))]
    public class ProcedureRepositoryTest
    {

        private readonly ProcedureRepositoryTestFixture _fixture;

        public ProcedureRepositoryTest(ProcedureRepositoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Insert))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Repositories")]
        public async Task Insert()
        {
            AbreuBarberDbContext dbContext = _fixture.CreateDbContext();
            var exampleProcedure = _fixture.GetExampleProcedure();
            var procedureRepository = new Repository.ProcedureRepository(dbContext);

            await procedureRepository.Insert(exampleProcedure, CancellationToken.None);
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var dbProcedure = await dbContext.Procedures.FindAsync(exampleProcedure.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Name.Should().Be(exampleProcedure.Name);
            dbProcedure.Description.Should().Be(exampleProcedure.Description);
            dbProcedure.Value.Should().Be(exampleProcedure.Value);
            dbProcedure.IsActive.Should().Be(exampleProcedure.IsActive);
            dbProcedure.CreatedAt.Should().Be(exampleProcedure.CreatedAt);
        }

        [Fact(DisplayName = nameof(Get))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Repositories")]
        public async Task Get()
        {
            AbreuBarberDbContext dbContext = _fixture.CreateDbContext();
            var exampleProcedure = _fixture.GetExampleProcedure();
            var exampleProceduresList = _fixture.GetExampleProceduresList(15);
            exampleProceduresList.Add(exampleProcedure);
            await dbContext.AddRangeAsync(exampleProceduresList);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            var procedureRepository = new Repository.ProcedureRepository(dbContext);

            var dbProcedure = await procedureRepository.Get(exampleProcedure.Id, CancellationToken.None);

            dbProcedure.Should().NotBeNull();
            dbProcedure.Id.Should().Be(exampleProcedure.Id);
            dbProcedure.Name.Should().Be(exampleProcedure.Name);
            dbProcedure.Description.Should().Be(exampleProcedure.Description);
            dbProcedure.Value.Should().Be(exampleProcedure.Value);
            dbProcedure.IsActive.Should().Be(exampleProcedure.IsActive);
            dbProcedure.CreatedAt.Should().Be(exampleProcedure.CreatedAt);
        }

        [Fact(DisplayName = nameof(GetThrowIfNotFound))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Repositories")]
        public async Task GetThrowIfNotFound()
        {
            AbreuBarberDbContext dbContext = _fixture.CreateDbContext();
            var exampleIdNotInList = Guid.NewGuid();
            await dbContext.AddRangeAsync(_fixture.GetExampleProceduresList(15));
            await dbContext.SaveChangesAsync(CancellationToken.None);
            var procedureRepository = new Repository.ProcedureRepository(dbContext);

            var task = async() =>  await procedureRepository.Get(exampleIdNotInList, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Procedure '{exampleIdNotInList}' not found");
        }
    }
}
