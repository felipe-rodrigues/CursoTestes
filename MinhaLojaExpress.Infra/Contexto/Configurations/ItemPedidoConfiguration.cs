using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Infra.Contexto.Configurations
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable("ItemsPedido");
            builder.HasKey(ip => new { ip.ItemId, ip.PedidoId });

            builder.Property(ip => ip.Quantidade)
                .IsRequired();

            builder.HasOne(ip => ip.Item)
                .WithMany()
                .HasForeignKey(ip => ip.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ip => ip.Pedido)
                .WithMany(p => p.Items)
                .HasForeignKey(ip => ip.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
