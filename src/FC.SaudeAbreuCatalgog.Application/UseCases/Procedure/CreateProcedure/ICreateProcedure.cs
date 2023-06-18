using MediatR;

namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure
{
    public interface ICreateProcedure : IRequestHandler<CreateProcedureInput,CreateProcedureOutput>
    {
        public Task<CreateProcedureOutput> Handle(CreateProcedureInput input, CancellationToken cancellationToken);
    }
}
