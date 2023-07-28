
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Xunit;

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
            var procedureRepository = new ProcedureRepository(dbContext);

            await procedureRepository.Insert(exampleProcedure, CancellationToken.None);
            await dbContext.SaveChangesAsync();

            var dbProcedure = await dbContext.Procedures.Find(exampleProcedure.Id);


            dbProcedure.Should().NotBeNull();
            dbProcedure.Name.Should().Be(exampleProcedure.Name);
            dbProcedure.Description.Should().Be(exampleProcedure.Description);
            dbProcedure.Value.Should().Be(exampleProcedure.Value);
            dbProcedure.IsAcive.Should().Be(exampleProcedure.IsActive);
            dbProcedure.CreatedAt.Should().Be(exampleProcedure.CreatedAt);
        }
    }
}
