using FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.Common;
using MediatR;

namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure
{
    public interface ICreateProcedure : IRequestHandler<CreateProcedureInput, ProcedureModelOutput>
    {
        public Task<ProcedureModelOutput> Handle(CreateProcedureInput input, CancellationToken cancellationToken);
    }
}
