

using FC.AbreuBarber.Application.Exceptions;
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

        public Task Delete(Procedure aggregate, CancellationToken _)
        {
            return Task.FromResult(_procedures.Remove(aggregate));
        }

        public async Task<Procedure> Get(Guid id, CancellationToken cancellationToken)
        {
            var procedure = await _procedures.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            NotFoundException.ThrowIfNull(procedure, $"Procedure '{id}' not found");

            return procedure!;
        }

        public async Task Insert(Procedure aggregate, CancellationToken cancellationToken) =>
            await _procedures.AddAsync(aggregate, cancellationToken);

        public Task<SearchOutput<Procedure>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(Procedure aggregate, CancellationToken _)
        {
            return Task.FromResult(_procedures.Update(aggregate));
        }
    }
}
