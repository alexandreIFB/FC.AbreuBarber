
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures
{
    public interface IListProcedures : IRequestHandler<ListProceduresInput,ListProceduresOutput>
    {
    }
}
