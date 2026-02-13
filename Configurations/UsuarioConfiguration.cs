
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaLogin.Models.User;

namespace PruebaLogin.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuarios2>
    {
        public void Configure(EntityTypeBuilder<Usuarios2> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nombre).HasMaxLength(100).IsRequired();
            builder.Property(u => u.UserName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(100).IsRequired();

            builder.Property(u => u.Activo).HasDefaultValue(true);

            // Relación con Rol
            builder.HasOne(u => u.Rol).WithMany(r => r.Usuarios).HasForeignKey(u => u.IdRol);
        }
    }
}
