using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Infra.Contexto.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Telefone).IsRequired().HasMaxLength(15);
            builder.HasMany(c => c.Pedidos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new List<Cliente>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Email = "meuemail@gmail.com",
                    Nome = "Cliente1",
                    Telefone = "31 91111-1111"
                }
            });
        }
    }
}
