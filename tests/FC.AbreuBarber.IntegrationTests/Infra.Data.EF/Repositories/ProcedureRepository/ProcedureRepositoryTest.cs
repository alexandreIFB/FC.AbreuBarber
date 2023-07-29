
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Repository = FC.AbreuBarber.Infra.Data.EF.Repositories;
using Xunit;
using FluentAssertions;
using FC.AbreuBarber.Application.Exceptions;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using FC.AbreuBarber.Domain.Entity;

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

            var dbProcedure = await _fixture.CreateDbContext(true).Procedures.FindAsync(exampleProcedure.Id);

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

        [Fact(DisplayName = nameof(Update))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Repositories")]
        public async Task Update()
        {
            AbreuBarberDbContext dbContext = _fixture.CreateDbContext();
            var exampleProcedure = _fixture.GetExampleProcedure();
            var newProcedureValues = _fixture.GetExampleProcedure();
            var exampleProceduresList = _fixture.GetExampleProceduresList(15);
            exampleProceduresList.Add(exampleProcedure);
            await dbContext.AddRangeAsync(exampleProceduresList);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            var procedureRepository = new Repository.ProcedureRepository(dbContext);

            exampleProcedure.Update(newProcedureValues.Name, newProcedureValues.Description, newProcedureValues.Value);

            await procedureRepository.Update(exampleProcedure, CancellationToken.None);

            await dbContext.SaveChangesAsync(CancellationToken.None);

            var dbProcedure = await _fixture.CreateDbContext(true).Procedures.FindAsync(exampleProcedure.Id);

            dbProcedure.Should().NotBeNull();
            dbProcedure!.Id.Should().Be(exampleProcedure.Id);
            dbProcedure.Name.Should().Be(exampleProcedure.Name);
            dbProcedure.Description.Should().Be(exampleProcedure.Description);
            dbProcedure.Value.Should().Be(exampleProcedure.Value);
            dbProcedure.IsActive.Should().Be(exampleProcedure.IsActive);
            dbProcedure.CreatedAt.Should().Be(exampleProcedure.CreatedAt);
        }

        [Fact(DisplayName = nameof(Delete))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Repositories")]
        public async Task Delete()
        {
            AbreuBarberDbContext dbContext = _fixture.CreateDbContext();
            var exampleProcedure = _fixture.GetExampleProcedure();
            var exampleProceduresList = _fixture.GetExampleProceduresList(15);
            exampleProceduresList.Add(exampleProcedure);
            await dbContext.AddRangeAsync(exampleProceduresList);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            var procedureRepository = new Repository.ProcedureRepository(dbContext);


            await procedureRepository.Delete(exampleProcedure, CancellationToken.None);

            await dbContext.SaveChangesAsync(CancellationToken.None);

            var dbProcedure = await _fixture.CreateDbContext(true).Procedures.FindAsync(exampleProcedure.Id);

            dbProcedure.Should().BeNull();
        }

        [Fact(DisplayName = nameof(SearchReturnListAndTotal))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Repositories")]
        public async Task SearchReturnListAndTotal()
        {
            AbreuBarberDbContext dbContext = _fixture.CreateDbContext();
            var exampleProceduresList = _fixture.GetExampleProceduresList(15);
            await dbContext.AddRangeAsync(exampleProceduresList);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            var procedureRepository = new Repository.ProcedureRepository(dbContext);

            var searchInput = new SearchInput(1,20,"", "", SearchOrder.Asc);

            var output = await procedureRepository.Search(searchInput, CancellationToken.None);

            output.Should().NotBeNull();
            output.Items.Should().NotBeNull().And.HaveCount(exampleProceduresList.Count);
            output.CurrentPage.Should().Be(searchInput.Page);
            output.PerPage.Should().Be(searchInput.PerPage);
            output.Total.Should().Be(exampleProceduresList.Count);
            foreach(Procedure outputItem in output.Items)
            {
                var exampleItem = exampleProceduresList.Find(
                    procedure => procedure.Id == outputItem.Id
                );
                exampleItem.Should().NotBeNull();
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(exampleItem!.Name);
                outputItem.Description.Should().Be(exampleItem.Description);
                outputItem.Value.Should().Be(exampleItem.Value);
                outputItem.IsActive.Should().Be(exampleItem.IsActive);
                outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
            }
        }

        [Fact(DisplayName = nameof(SearchReturnEmptyWhenPersistenceIsEmpty))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Repositories")]
        public async Task SearchReturnEmptyWhenPersistenceIsEmpty()
        {
            AbreuBarberDbContext dbContext = _fixture.CreateDbContext();
            var procedureRepository = new Repository.ProcedureRepository(dbContext);

            var searchInput = new SearchInput(1, 20, "", "", SearchOrder.Asc);

            var output = await procedureRepository.Search(searchInput, CancellationToken.None);

            output.Should().NotBeNull();
            output.Items.Should().NotBeNull().And.HaveCount(0);
            output.CurrentPage.Should().Be(searchInput.Page);
            output.PerPage.Should().Be(searchInput.PerPage);
            output.Total.Should().Be(0);
        }

        [Theory(DisplayName = nameof(SearchReturnListAndTotal))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Repositories")]
        [InlineData(10, 1, 5, 5)]
        [InlineData(27, 3, 10, 7)]
        [InlineData(17, 2, 5, 5)]
        [InlineData(19, 4, 5, 4)]
        public async Task SearchReturnsPaginated(
            int quantityProceduresToGenerate,
            int page,
            int perPage,
            int expectedQuantityItems
            )
        {
            AbreuBarberDbContext dbContext = _fixture.CreateDbContext();
            var exampleProceduresList = _fixture.GetExampleProceduresList(quantityProceduresToGenerate);
            await dbContext.AddRangeAsync(exampleProceduresList);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            var procedureRepository = new Repository.ProcedureRepository(dbContext);

            var searchInput = new SearchInput(page, perPage, "", "", SearchOrder.Asc);

            var output = await procedureRepository.Search(searchInput, CancellationToken.None);

            output.Should().NotBeNull();
            output.CurrentPage.Should().Be(searchInput.Page);
            output.PerPage.Should().Be(searchInput.PerPage);
            output.Total.Should().Be(quantityProceduresToGenerate);
            output.Items.Should().NotBeNull().And.HaveCount(expectedQuantityItems);
            foreach (Procedure outputItem in output.Items)
            {
                var exampleItem = exampleProceduresList.Find(
                    procedure => procedure.Id == outputItem.Id
                );
                exampleItem.Should().NotBeNull();
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(exampleItem!.Name);
                outputItem.Description.Should().Be(exampleItem.Description);
                outputItem.Value.Should().Be(exampleItem.Value);
                outputItem.IsActive.Should().Be(exampleItem.IsActive);
                outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
            }
        }

        [Theory(DisplayName = nameof(SearchByText))]
        [Trait("Integration/Infra.Data", "ProcedureRepository - Repositories")]
        [InlineData("Corte", 1, 5, 5, 6)]
        [InlineData("Corte", 2, 5, 1, 6)]
        [InlineData("Barba", 1, 5, 5, 7)]
        [InlineData("Barba", 2, 5, 2, 7)]
        [InlineData("Barba", 3, 5, 0, 7)]
        public async Task SearchByText(
            string search,
            int page,
            int perPage,
            int expectedQuantityItemsReturned,
            int expectedQuantityItemsTotal
            )
        {
            AbreuBarberDbContext dbContext = _fixture.CreateDbContext();
            var exampleProceduresList = _fixture.GetExampleProceduresListWithNames(new List<string>
        {
            "Corte de Cabelo",
            "Barba Tradicional",
            "Barba Moderna",
            "Penteado Clássico",
            "Penteado com Pomada",
            "Desenho de Barba",
            "Corte Degradê",
            "Corte Social",
            "Corte de Máquina",
            "Tintura de Barba",
            "Hidratação de Barba",
            "Massagem Capilar",
            "Design de Sobrancelhas",
            "Aplicação de Cera Quente",
            "Aparar Bigode",
            "Depilação Facial",
            "Tratamento para Queda de Cabelo",
            "Barboterapia (Barba + Hidratação)",
            "Relaxamento Capilar",
            "Touca de Gesso",
            "Corte Infantil",
            "Corte Feminino",
            "Sobrancelhas Masculinas",
            "Dreadlocks",
            "Trança Masculina",
            "Acabamento na Nuca",
            "Barba Feita na Navalha"
        });
            await dbContext.AddRangeAsync(exampleProceduresList);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            var procedureRepository = new Repository.ProcedureRepository(dbContext);

            var searchInput = new SearchInput(page, perPage, search, "", SearchOrder.Asc);

            var output = await procedureRepository.Search(searchInput, CancellationToken.None);

            output.Should().NotBeNull();
            output.CurrentPage.Should().Be(searchInput.Page);
            output.PerPage.Should().Be(searchInput.PerPage);
            output.Total.Should().Be(expectedQuantityItemsTotal);
            output.Items.Should().NotBeNull().And.HaveCount(expectedQuantityItemsReturned);
            foreach (Procedure outputItem in output.Items)
            {
                var exampleItem = exampleProceduresList.Find(
                    procedure => procedure.Id == outputItem.Id
                );
                exampleItem.Should().NotBeNull();
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(exampleItem!.Name);
                outputItem.Description.Should().Be(exampleItem.Description);
                outputItem.Value.Should().Be(exampleItem.Value);
                outputItem.IsActive.Should().Be(exampleItem.IsActive);
                outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
            }
        }
    }
}
