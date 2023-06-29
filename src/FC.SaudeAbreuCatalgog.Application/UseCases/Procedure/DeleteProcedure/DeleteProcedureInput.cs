

using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure
{
    public class DeleteProcedureInput : IRequest
    {
        public Guid Id { get; set; }

        public DeleteProcedureInput(Guid id)
        {
            Id = id;
        }
    }
}
