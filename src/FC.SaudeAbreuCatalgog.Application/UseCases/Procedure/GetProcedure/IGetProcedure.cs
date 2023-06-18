using FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.Common;
using MediatR;


namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.GetProcedure
{
    public interface IGetProcedure : IRequestHandler<GetProcedureInput, ProcedureModelOutput>
    {
        public Task<ProcedureModelOutput> Handle(GetProcedureInput input,CancellationToken cancellationToken);
    }
}
