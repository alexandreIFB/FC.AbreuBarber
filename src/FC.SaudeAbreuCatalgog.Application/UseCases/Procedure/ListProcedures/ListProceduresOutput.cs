using FC.AbreuBarber.Application.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures
{
    public class ListProceduresOutput 
        : PaginatedListOutput<ProcedureModelOutput>,
        IRequest
    {
        public ListProceduresOutput(
            int page,
            int perPage,
            int total,
            int lastPage,
            IReadOnlyList<ProcedureModelOutput> items) 
            : base(page, perPage, total, lastPage, items)
        {

        }
    }
}
