

using Moq;
using Xunit;
using DomainEntity = FC.AbreuBarber.Domain.Entity;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.ListProcedures
{
    public class ListProceduresTest
    {

        private readonly ListProceduresTestFixture _fixture;
        public ListProceduresTest(ListProceduresTestFixture fixture) => _fixture = fixture;

        [Fact(DisplayName = nameof(List))]
        [Trait("Application", "ListProcedures - Use Cases")]
        public async Task List()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var proceduresList = _fixture.GetExampleProceduresList();
        
            var input = new ListProceduresInput(
                page: 2,
                perPage: 15,
                search: "search-example",
                sort: "name",
                dir: SearchOrder.Asc
            );

            var outputRepositorySearch = new OutputSearch<DomainEntity.Procedure>(
                    currentPage: input.Page,
                    perPage: input.PerPage,
                    lastPage: 7,
                    total: 72,
                    Items: (IReadOnlyList<DomainEntity.Procedure>)proceduresList
             );




            repositoryMock.Setup(x => x.Search(
                    It.Is<SearchInput>(
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && serachInput.OrderBy == input.Sort
                        && searchInput.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>
             )).ReturnsAsync(outputRepositorySearch);

            var useCase = new ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.currentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.LastPage.Should().Be(outputRepositorySearch.LastPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            output.Items.Foreach(outputItem =>
            {
                var repositoryProcedure = outputRepositorySearch.Items.Find(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryProcedure.Name);
                outputItem.Description.Should().Be(repositoryProcedure.Description);
                outputItem.Value.Should().Be(repositoryProcedure.Value);
                outputItem.IsActive.Should().Be(repositoryProcedure.IsActive);
                outputItem.Id.Should().NotBeEmpty();
                outputItem.CreatedAt.Should().NotBeSameDateAs(default);
            });

            repositoryMock.Verify(x => x.Search(
                    It.Is<SearchInput>(
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && serachInput.OrderBy == input.Sort
                        && searchInput.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>
             ), Times.Once);
        }

    }
}
