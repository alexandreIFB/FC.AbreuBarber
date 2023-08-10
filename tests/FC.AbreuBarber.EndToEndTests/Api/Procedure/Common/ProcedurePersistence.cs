

using FC.AbreuBarber.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;
using DomainEntity = FC.AbreuBarber.Domain.Entity;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.Common
{
    public class ProcedurePersistence
    {

        private readonly AbreuBarberDbContext _dbContext;

        public ProcedurePersistence(AbreuBarberDbContext dbContext)
            =>  _dbContext = dbContext;

        public async Task<DomainEntity.Procedure?> GetById(Guid id)
            => await _dbContext
            .Procedures.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
