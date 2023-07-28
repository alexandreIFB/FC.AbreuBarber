using FC.AbreuBarber.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FC.AbreuBarber.Infra.Data.EF.Configurations
{
    public class AbreuBarberDbContext : DbContext
    {
        public DbSet<Procedure> Procedures => Set<Procedure>();

        public AbreuBarberDbContext(
            DbContextOptions<AbreuBarberDbContext> options
            ): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProcedureConfiguration());
        }
    }
}
