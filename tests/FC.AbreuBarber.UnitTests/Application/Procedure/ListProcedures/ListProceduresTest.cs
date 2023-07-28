

using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure;
using FluentAssertions;
using Moq;
using Xunit;
using DomainEntity = FC.AbreuBarber.Domain.Entity;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;


namespace FC.AbreuBarber.UnitTests.Application.Procedure.ListProcedures
{
    [Collection(nameof(ListProceduresTestFixture))]
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
            var input = _fixture.GetExampleInput();

            var outputRepositorySearch = new SearchOutput<DomainEntity.Procedure>(
                    currentPage: input.Page,
                    perPage: input.PerPage,
                    total: (new Random()).Next(50,200),
                    items: proceduresList
             );

            repositoryMock.Setup(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                        && searchInput.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>()
             )).ReturnsAsync(outputRepositorySearch);

            var useCase = new UseCase.ListProcedures(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);


            var outputLastPage = outputRepositorySearch.Total / (double)(output.PerPage);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.LastPage.Should().Be((int)Math.Ceiling(outputLastPage));
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            ((List<ProcedureModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryProcedure = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryProcedure!.Name);
                outputItem.Description.Should().Be(repositoryProcedure.Description);
                outputItem.Value.Should().Be(repositoryProcedure.Value);
                outputItem.IsActive.Should().Be(repositoryProcedure.IsActive);
                outputItem.Id.Should().NotBeEmpty();
                outputItem.CreatedAt.Should().NotBeSameDateAs(default);
            });

            repositoryMock.Verify(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                        && searchInput.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>()
             ), Times.Once);
        }

        [Theory(DisplayName = nameof(ListInputWithoutAllParameters))]
        [Trait("Application", "ListProcedures - Use Cases")]
        [MemberData(
            nameof(ListProceduresTestDataGenerator.GetInputsWithoutAllParameter),
            parameters: 15,
            MemberType = typeof(ListProceduresTestDataGenerator)
            )]
        public async Task ListInputWithoutAllParameters(UseCase.ListProceduresInput input)
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var proceduresList = _fixture.GetExampleProceduresList();

            var outputRepositorySearch = new SearchOutput<DomainEntity.Procedure>(
                    currentPage: input.Page,
                    perPage: input.PerPage,
                    total: (new Random()).Next(50, 200),
                    items: proceduresList
             );

            repositoryMock.Setup(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                        && searchInput.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>()
             )).ReturnsAsync(outputRepositorySearch);

            var useCase = new UseCase.ListProcedures(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);


            var outputLastPage = outputRepositorySearch.Total / (double)(output.PerPage);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.LastPage.Should().Be((int)Math.Ceiling(outputLastPage));
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            ((List<ProcedureModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryProcedure = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryProcedure!.Name);
                outputItem.Description.Should().Be(repositoryProcedure.Description);
                outputItem.Value.Should().Be(repositoryProcedure.Value);
                outputItem.IsActive.Should().Be(repositoryProcedure.IsActive);
                outputItem.Id.Should().NotBeEmpty();
                outputItem.CreatedAt.Should().NotBeSameDateAs(default);
            });

            repositoryMock.Verify(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                        && searchInput.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>()
             ), Times.Once);
        }

        [Fact(DisplayName = nameof(ListOkWhenEmpty))]
        [Trait("Application", "ListProcedures - Use Cases")]
        public async Task ListOkWhenEmpty()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var proceduresListEmpty = new List<DomainEntity.Procedure>();
            var input = _fixture.GetExampleInput();

            var outputRepositorySearch = new SearchOutput<DomainEntity.Procedure>(
                    currentPage: input.Page,
                    perPage: input.PerPage,
                    total: 0,
                    items: proceduresListEmpty
             );

            repositoryMock.Setup(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                        && searchInput.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>()
             )).ReturnsAsync(outputRepositorySearch);

            var useCase = new UseCase.ListProcedures(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);


            var outputLastPage = outputRepositorySearch.Total / (double)(output.PerPage);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.LastPage.Should().Be((int)Math.Ceiling(outputLastPage));
            output.Total.Should().Be(0);
            output.Items.Should().HaveCount(0);
            
            repositoryMock.Verify(x => x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                        && searchInput.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>()
             ), Times.Once);
        }

    }
}
