using FC.SaudeAbreuCatalgog.Domain.Entity;

namespace FC.SaudeAbreuCatalgog.Domain.SeedWork
{
    public interface IGenericRepository<TAggregate> : IRepository
    {
        public Task Insert(TAggregate aggregate, CancellationToken cancellationToken);
        public Task<Procedure> Get(Guid id, CancellationToken cancellationToken);
    }
}
