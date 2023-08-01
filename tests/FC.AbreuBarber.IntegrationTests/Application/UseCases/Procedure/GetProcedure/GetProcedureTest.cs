

using UseCase = FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure;
using Xunit;
using FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure;
using FluentAssertions;
using FC.AbreuBarber.Infra.Data.EF.Repositories;
using FC.AbreuBarber.Application.Exceptions;

namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.GetProcedure
{
    [Collection(nameof(GetProcedureTestFixture))] 
    public class GetProcedureTest
    {
        private readonly GetProcedureTestFixture _fixture;

        public GetProcedureTest(GetProcedureTestFixture fixture) 
            => _fixture = fixture;

        [Fact(DisplayName = nameof(GetProcedure))]
        [Trait("Integration/Application", "GetProcedure - Use Cases")]
        public async Task GetProcedure()
        {
            var exampleProcedure = _fixture.GetExampleProcedure();

            var dbContext = _fixture.CreateDbContext();
            dbContext.Add(exampleProcedure);
            dbContext.SaveChanges();

            var repository = new ProcedureRepository(dbContext);
            var getUseCase = new UseCase.GetProcedure(repository);

            var input = new GetProcedureInput(exampleProcedure.Id);
            
            var output = await getUseCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(exampleProcedure.Name);
            output.Description.Should().Be(exampleProcedure.Description);
            output.Value.Should().Be(exampleProcedure.Value);
            output.IsActive.Should().Be(exampleProcedure.IsActive);
            output.Id.Should().Be(exampleProcedure.Id);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default);
        }

        [Fact(DisplayName = nameof(NotFoundExceptionProcedure))]
        [Trait("Integration/Application", "GetProcedure - Use Cases")]
        public async void NotFoundExceptionProcedure()
        {
            var exampleProcedure = _fixture.GetExampleProcedure();

            var dbContext = _fixture.CreateDbContext();
            dbContext.Add(exampleProcedure);
            dbContext.SaveChanges();

            var repository = new ProcedureRepository(dbContext);

            var getUseCase = new UseCase.GetProcedure(repository);

            var exampleGuid = Guid.NewGuid();
            var input = new GetProcedureInput(exampleGuid);

            Func<Task> task = async () => await getUseCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Procedure '{exampleGuid}' not found");
        }
    }
}
