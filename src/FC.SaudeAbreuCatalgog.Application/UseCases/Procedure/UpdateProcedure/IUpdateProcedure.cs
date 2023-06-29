using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure
{
    public interface IUpdateProcedure : IRequestHandler<UpdateProcedureInput, ProcedureModelOutput>
    {
        public Task<ProcedureModelOutput> Handle(UpdateProcedureInput input, CancellationToken cancellationToken);
    }
}
