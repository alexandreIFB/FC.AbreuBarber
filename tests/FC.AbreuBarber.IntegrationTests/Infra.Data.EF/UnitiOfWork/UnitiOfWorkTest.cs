using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

using UnitOfWorkInfra = FC.AbreuBarber.Infra.Data.EF;

namespace FC.AbreuBarber.IntegrationTests.Infra.Data.EF.UnitiOfWork
{
    [Collection(nameof(UnitiOfWorkTestFixture))]
    public class UnitiOfWorkTest
    {
        private readonly UnitiOfWorkTestFixture _fixture;

        public UnitiOfWorkTest(UnitiOfWorkTestFixture fixture) =>
            _fixture = fixture;

        [Fact(DisplayName = nameof(Commit))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Persistence")]
        public async Task Commit()
        {
            var dbContext = _fixture.CreateDbContext();
            var exampleCategoriesList = _fixture.GetExampleProceduresList();
            await dbContext.AddRangeAsync(exampleCategoriesList);

            var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

            await unitOfWork.Commit(CancellationToken.None);

            var assertDbContext = _fixture.CreateDbContext(true);
            var savedProcedures = assertDbContext.Procedures.AsNoTracking().ToList();

            savedProcedures.Should().HaveCount(exampleCategoriesList.Count);
        }

        [Fact(DisplayName = nameof(Rollback))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Persistence")]
        public async Task Rollback()
        {
            var dbContext = _fixture.CreateDbContext();

            var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

            var task = async () => await unitOfWork.Rollback(CancellationToken.None);

            await task.Should().NotThrowAsync();
        }
    }
}
