

using FC.AbreuBarber.Application.Common;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures
{
    public class ListProceduresInput : PaginatedListInput, IRequest<ListProceduresOutput>
    {
        public ListProceduresInput(
            int page,
            int perPage,
            string search,
            string sort,
            SearchOrder dir)
            : base(page, perPage, search, sort, dir)
        {
        }
    }
}
