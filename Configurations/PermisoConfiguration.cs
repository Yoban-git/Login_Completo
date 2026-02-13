
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaLogin.Models.Permisos;

namespace PruebaLogin.Configurations
{
    public class PermisoConfiguration : IEntityTypeConfiguration<Permisos2>
    {
        public void Configure(EntityTypeBuilder<Permisos2> builder)
        {
            builder.ToTable("Permisos");
            builder.HasKey(p => p.IdPermiso);
            builder.Property(p => p.NombrePermiso).HasMaxLength(50).IsRequired();

            builder.HasIndex(p => p.NombrePermiso).IsUnique();

        }
    }
}
