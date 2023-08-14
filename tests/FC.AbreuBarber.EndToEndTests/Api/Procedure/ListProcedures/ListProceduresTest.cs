
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using System.Net;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.ListProcedures
{

    [Collection(nameof(ListProceduresTestFixture))]
    public class ListProceduresTest : IDisposable
    {

        private readonly ListProceduresTestFixture _fixture;

        public ListProceduresTest(ListProceduresTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(ListProceduresWithInputDefault))]
        [Trait("End2End/API", "Procedure/List - Endpoints")]
        public async Task ListProceduresWithInputDefault()
        {
            var defaultPage = 1;
            int defaultPerPage = 10;
            
            var exampleProceduresList = _fixture.GetExampleProceduresList(20);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var (response, output) = await _fixture.
                ApiClient.Get<ListProceduresOutput>(
                "/procedures"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Page.Should().Be(defaultPage);
            output.PerPage.Should().Be(defaultPerPage);
            output.Total.Should().Be(exampleProceduresList.Count);
            output.Items.Should().NotBeNull().And.HaveCount(defaultPerPage);
            foreach (ProcedureModelOutput outputItem in output.Items)
            {
                var exampleItem = exampleProceduresList.FirstOrDefault(
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



        [Fact(DisplayName = nameof(ListWhenEmpty))]
        [Trait("End2End/API", "Procedure/List - Endpoints")]
        public async Task ListWhenEmpty()
        {
            var defaultPage = 1;
            int defaultPerPage = 10;

            var (response, output) = await _fixture.
                ApiClient.Get<ListProceduresOutput>(
                "/procedures"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Page.Should().Be(defaultPage);
            output.PerPage.Should().Be(defaultPerPage);
            output.Total.Should().Be(0);
            output.Items.Should().NotBeNull().And.HaveCount(0);
        }

        [Fact(DisplayName = nameof(ListProcedures))]
        [Trait("End2End/API", "Procedure/List - Endpoints")]
        public async Task ListProcedures()
        {
            var exampleProceduresList = _fixture.GetExampleProceduresList(20);

            var input = new ListProceduresInput(2,5);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var (response, output) = await _fixture.
                ApiClient.Get<ListProceduresOutput>(
                "/procedures", input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Page.Should().Be(input.Page);
            output.PerPage.Should().Be(input.PerPage);
            output.Total.Should().Be(exampleProceduresList.Count);
            output.Items.Should().NotBeNull().And.HaveCount(input.PerPage);
            foreach (ProcedureModelOutput outputItem in output.Items)
            {
                var exampleItem = exampleProceduresList.FirstOrDefault(
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


        [Theory(DisplayName = nameof(ListProcedures))]
        [Trait("End2End/API", "Procedure/List - Endpoints")]
        [InlineData(10, 1, 5, 5)]
        [InlineData(27, 3, 10, 7)]
        [InlineData(17, 2, 5, 5)]
        [InlineData(19, 4, 5, 4)]
        public async Task ListProceduresPaginated(
            int quantityProceduresToGenerate,
            int page,
            int perPage,
            int expectedQuantityItems
            )
        {
            var exampleProceduresList = _fixture.GetExampleProceduresList(quantityProceduresToGenerate);

            var input = new ListProceduresInput(page, perPage, "", "", SearchOrder.Asc);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var (response, output) = await _fixture.
                ApiClient.Get<ListProceduresOutput>(
                "/procedures", input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Page.Should().Be(page);
            output.PerPage.Should().Be(perPage);
            output.Total.Should().Be(quantityProceduresToGenerate);
            output.Items.Should().NotBeNull().And.HaveCount(expectedQuantityItems);
            foreach (ProcedureModelOutput outputItem in output.Items)
            {
                var exampleItem = exampleProceduresList.FirstOrDefault(
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

        public void Dispose()
        {
            _fixture.CleanPersistence();
        }

    }
}
