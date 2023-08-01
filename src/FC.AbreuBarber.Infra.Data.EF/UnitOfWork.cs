using FC.AbreuBarber.Application.Interfaces;
using FC.AbreuBarber.Infra.Data.EF.Configurations;

namespace FC.AbreuBarber.Infra.Data.EF
{
    public class UnitOfWork : IUnityOfWork
    {
        private readonly AbreuBarberDbContext _context;

        public UnitOfWork(AbreuBarberDbContext context) 
            => _context = context;

        public Task Commit(CancellationToken cancellationToken)
        {
           return _context.SaveChangesAsync(cancellationToken);
        }

        public Task Rollback(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
