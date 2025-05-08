using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Infra.Contexto.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        { 
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Items)
                .WithOne()
                .HasForeignKey("PedidoId") // Chave estrangeira na tabela ItemPedido
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Descontos)
                .WithMany(d => d.Pedidos)
                .UsingEntity<Dictionary<string, object>>(
                    "PedidoDesconto",
                    j => j.HasOne<Desconto>().WithMany().HasForeignKey("DescontoId"),
                    j => j.HasOne<Pedido>().WithMany().HasForeignKey("PedidoId")
                );

            builder.Property(p => p.ValorTotal)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.DataPedido)
                .IsRequired();
        }
    }
}
