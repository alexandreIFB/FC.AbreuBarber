using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure
{
    public class GetProcedureInput : IRequest<ProcedureModelOutput>
    {
        public Guid Id { get; set; }

        public GetProcedureInput(Guid id)
        {
            Id = id;
        }
    }
}
