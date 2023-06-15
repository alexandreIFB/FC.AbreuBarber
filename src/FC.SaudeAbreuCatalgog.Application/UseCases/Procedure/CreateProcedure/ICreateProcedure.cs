
namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure
{
    public interface ICreateProcedure
    {
        public Task<CreateProcedureOutput> Handle(CreateProcedureInput input, CancellationToken cancellationToken);
    }
}
