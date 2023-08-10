
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using FC.AbreuBarber.Infra.Data.EF.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;


namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.ListProcedures
{
    [Collection(nameof(ListProceduresTestFixture))]
    public class ListProceduresTest
    {

        private readonly ListProceduresTestFixture _fixture;

        public ListProceduresTest(ListProceduresTestFixture fixture)
            => _fixture = fixture;

        [Theory(DisplayName = nameof(List))]
        [Trait("Integration/Application", "ListProcedures - Use Cases")]
        [InlineData(10, 1, 5, 5)]
        [InlineData(27, 3, 10, 7)]
        [InlineData(17, 2, 5, 5)]
        [InlineData(19, 4, 5, 4)]
        public async Task List(
            int quantityProceduresToGenerate,
            int page,
            int perPage,
            int expectedQuantityItems
            )
        {
            var exampleProceduresList = _fixture.GetExampleProceduresList(quantityProceduresToGenerate);
            var dbContext = _fixture.CreateDbContext();
            await dbContext.AddRangeAsync(exampleProceduresList);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            var procedureRepository = new ProcedureRepository(dbContext);

            var listInput = new UseCase.ListProceduresInput(page, perPage, "", "", SearchOrder.Asc);

            var useCase = new UseCase.ListProcedures(procedureRepository);

            var output = await useCase.Handle(listInput, CancellationToken.None);

            output.Should().NotBeNull();
            output.Items.Should().NotBeNull().And.HaveCount(expectedQuantityItems);
            output.Page.Should().Be(listInput.Page);
            output.PerPage.Should().Be(listInput.PerPage);
            output.Total.Should().Be(quantityProceduresToGenerate);
            foreach (ProcedureModelOutput outputItem in output.Items)
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

        [Fact(DisplayName = nameof(ListOkWhenEmpty))]
        [Trait("Integration/Application", "ListProcedures - Use Cases")]
        public async Task ListOkWhenEmpty()
        {

            var dbContext = _fixture.CreateDbContext();
            var procedureRepository = new ProcedureRepository(dbContext);

            var listInput = new UseCase.ListProceduresInput(1, 10, "", "", SearchOrder.Asc);

            var useCase = new UseCase.ListProcedures(procedureRepository);

            var output = await useCase.Handle(listInput, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(listInput.Page);
            output.PerPage.Should().Be(listInput.PerPage);
            output.Total.Should().Be(0);
            output.Items.Should().HaveCount(0);
        }
    }
}
