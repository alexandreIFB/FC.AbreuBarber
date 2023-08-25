
using Fc.AbreuBarber.Api.ApiModels.Response;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using FC.AbreuBarber.EndToEndTests.Extensions.DateTime;
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
                ApiClient.Get<ListApiResponse<IReadOnlyList<ProcedureModelOutput>>>(
                "/procedures"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Data.Should().NotBeNull();
            output!.Meta.Should().NotBeNull();
            output.Meta.Page.Should().Be(defaultPage);
            output.Meta.PerPage.Should().Be(defaultPerPage);
            output.Meta.Total.Should().Be(exampleProceduresList.Count);
            output.Data.Should().NotBeNull().And.HaveCount(defaultPerPage);
            foreach (ProcedureModelOutput outputItem in output.Data)
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
                outputItem.CreatedAt.TrimMillisseconds().Should().Be(
                    exampleItem.CreatedAt.TrimMillisseconds()
                );
            }
        }



        [Fact(DisplayName = nameof(ListWhenEmpty))]
        [Trait("End2End/API", "Procedure/List - Endpoints")]
        public async Task ListWhenEmpty()
        {
            var defaultPage = 1;
            int defaultPerPage = 10;

            var (response, output) = await _fixture.
                ApiClient.Get<ListApiResponse<IReadOnlyList<ProcedureModelOutput>>>(
                "/procedures"
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Meta.Should().NotBeNull();
            output.Data.Should().NotBeNull();
            output.Meta.Page.Should().Be(defaultPage);
            output.Meta.Should().Be(defaultPerPage);
            output.Meta.Total.Should().Be(0);
            output.Data.Should().NotBeNull().And.HaveCount(0);
        }

        [Fact(DisplayName = nameof(ListProcedures))]
        [Trait("End2End/API", "Procedure/List - Endpoints")]
        public async Task ListProcedures()
        {
            var exampleProceduresList = _fixture.GetExampleProceduresList(20);

            var input = new ListProceduresInput(2,5);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var (response, output) = await _fixture.
                ApiClient.Get<ListApiResponse<IReadOnlyList<ProcedureModelOutput>>>(
                "/procedures", input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Meta.Should().NotBeNull();
            output.Data.Should().NotBeNull();
            output.Meta.Page.Should().Be(input.Page);
            output.Meta.Should().Be(input.PerPage);
            output.Meta.Total.Should().Be(exampleProceduresList.Count);
            output.Data.Should().NotBeNull().And.HaveCount(input.PerPage);
            foreach (ProcedureModelOutput outputItem in output.Data)
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
                outputItem.CreatedAt.TrimMillisseconds().Should().Be(
                    exampleItem.CreatedAt.TrimMillisseconds()
                );
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
                ApiClient.Get<ListApiResponse<IReadOnlyList<ProcedureModelOutput>>>(
                "/procedures", input
            );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.OK);
            output.Should().NotBeNull();
            output!.Data.Should().NotBeNull();
            output.Meta.Should().NotBeNull();
            output.Meta.Page.Should().Be(page);
            output.Meta.PerPage.Should().Be(perPage);
            output.Meta.Total.Should().Be(quantityProceduresToGenerate);
            output.Data.Should().NotBeNull().And.HaveCount(expectedQuantityItems);
            foreach (ProcedureModelOutput outputItem in output.Data)
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
                outputItem.CreatedAt.TrimMillisseconds().Should().Be(
                    exampleItem.CreatedAt.TrimMillisseconds()
                );
            }
        }

        public void Dispose()
        {
            _fixture.CleanPersistence();
        }

    }
}
