
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaLogin.Models.Roles;

namespace PruebaLogin.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Roles2>
    {
        public void Configure(EntityTypeBuilder<Roles2> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(r => r.IdRol);
            builder.Property(r => r.NombreRol).HasMaxLength(100).IsRequired();
            builder.HasIndex(r => r.NombreRol).IsUnique();
        }
    }
}
