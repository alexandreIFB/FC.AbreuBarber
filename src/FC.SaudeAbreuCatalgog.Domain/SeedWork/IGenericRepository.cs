using FC.AbreuBarber.Domain.Entity;

namespace FC.AbreuBarber.Domain.SeedWork
{
    public interface IGenericRepository<TAggregate> : IRepository
    {
        public Task Insert(TAggregate aggregate, CancellationToken cancellationToken);
        public Task<Procedure> Get(Guid id, CancellationToken cancellationToken);
    }
}
