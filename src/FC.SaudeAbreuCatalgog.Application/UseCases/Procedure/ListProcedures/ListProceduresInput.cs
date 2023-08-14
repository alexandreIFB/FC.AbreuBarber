

using FC.AbreuBarber.Application.Common;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures
{
    public class ListProceduresInput : PaginatedListInput, IRequest<ListProceduresOutput>
    {
        public ListProceduresInput(
            int page = 1,
            int perPage = 10,
            string search = "",
            string sort = "",
            SearchOrder dir = SearchOrder.Asc)
            : base(page, perPage, search, sort, dir)
        { }

        public ListProceduresInput()
            : base(1, 10, "", "", SearchOrder.Asc)
        { }
    }
}
