

using FC.AbreuBarber.Domain.Entity;
using FC.AbreuBarber.Domain.Repository;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FC.AbreuBarber.Infra.Data.EF.Repositories
{
    public class ProcedureRepository : IProcedureRepository
    {

        private readonly AbreuBarberDbContext _dbContext;
        private DbSet<Procedure> _procedures => _dbContext.Set<Procedure>();

        public ProcedureRepository(AbreuBarberDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Delete(Procedure aggregate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Procedure> Get(Guid id, CancellationToken cancellationToken) =>
            await _procedures.FindAsync(id, cancellationToken);

        public async Task Insert(Procedure aggregate, CancellationToken cancellationToken) =>
            await _procedures.AddAsync(aggregate, cancellationToken);

        public Task<SearchOutput<Procedure>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(Procedure aggregate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
