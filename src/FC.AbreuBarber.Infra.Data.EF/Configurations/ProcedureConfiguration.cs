using FC.AbreuBarber.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.AbreuBarber.Infra.Data.EF.Configurations
{
    internal class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.HasKey(procedure => procedure.Id);
            builder.Property(procedure => procedure.Name).HasMaxLength(255);
            builder.Property(procedure => procedure.Description).HasMaxLength(10_000);
        }
    }
}
