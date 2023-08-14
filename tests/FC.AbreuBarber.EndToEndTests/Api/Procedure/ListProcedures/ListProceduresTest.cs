
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;
using FluentAssertions;
using System.Net;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.ListProcedures
{

    [Collection(nameof(ListProceduresTestFixture))]
    public class ListProceduresTest
    {

        private readonly ListProceduresTestFixture _fixture;

        public ListProceduresTest(ListProceduresTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(ListProceduresWithInputDefault))]
        [Trait("End2End/API", "Procedure/List - Endpoints")]
        public async void ListProceduresWithInputDefault()
        {
            var defaultPage = 1;
            int defaultPerPage = 10;
            

            var exampleProceduresList = _fixture.GetExampleProceduresList(20);

            await _fixture.Persistence.InsertList(exampleProceduresList);

            var expectedGetProcedure = exampleProceduresList[10];

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

    }
}
