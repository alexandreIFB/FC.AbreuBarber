
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Repository = FC.AbreuBarber.Infra.Data.EF.Repositories;
using Xunit;
using FluentAssertions;

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
            await dbContext.SaveChangesAsync();

            var dbProcedure = await dbContext.Procedures.FindAsync(exampleProcedure.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Name.Should().Be(exampleProcedure.Name);
            dbProcedure.Description.Should().Be(exampleProcedure.Description);
            dbProcedure.Value.Should().Be(exampleProcedure.Value);
            dbProcedure.IsActive.Should().Be(exampleProcedure.IsActive);
            dbProcedure.CreatedAt.Should().Be(exampleProcedure.CreatedAt);
        }
    }
}
