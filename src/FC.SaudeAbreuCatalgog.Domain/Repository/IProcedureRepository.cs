using FC.AbreuBarber.Domain.Entity;
using FC.AbreuBarber.Domain.SeedWork;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;

namespace FC.AbreuBarber.Domain.Repository
{
    public interface IProcedureRepository 
        : IGenericRepository<Procedure>,
        ISearchableRepository<Procedure>
    { }
}