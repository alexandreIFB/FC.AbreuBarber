

using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Domain.Repository;

namespace FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures
{
    public class ListProcedures : IListProcedures
    {
        private readonly IProcedureRepository _procedureRepository;

        public ListProcedures(IProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }

        public async Task<ListProceduresOutput> Handle(
            ListProceduresInput request,
            CancellationToken cancellationToken)
        {
            var searchOutput = await _procedureRepository.Search(
                new(
                    page: request.Page,
                    perPage: request.PerPage,
                    search: request.Search,
                    orderBy: request.Sort,
                    order: request.Dir
                    ), 
                cancellationToken);
            var lastPage =  (int)Math.Ceiling(searchOutput.Total / (double)searchOutput.PerPage);

            return new ListProceduresOutput(
                searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                lastPage,
                searchOutput.Items.Select(ProcedureModelOutput.FromProcedure).ToList()
                );
        }
    }
}
