using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure
{
    public interface ICreateProcedure : IRequestHandler<CreateProcedureInput, ProcedureModelOutput>
    {
        public Task<ProcedureModelOutput> Handle(CreateProcedureInput input, CancellationToken cancellationToken);
    }
}
