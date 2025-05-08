using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Infra.Contexto.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Nome).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Quantidade).IsRequired();
            builder.Property(i => i.Preco).IsRequired().HasColumnType("decimal(10,2)");
            builder.HasMany(i => i.Descontos)
                .WithMany(d => d.Items)
                .UsingEntity<Dictionary<string, object>>(
                    "ItemDesconto",
                    j => j.HasOne<Desconto>().WithMany().HasForeignKey("DescontoId"),
                    j => j.HasOne<Item>().WithMany().HasForeignKey("ItemId")
                );

            builder.HasData(new List<Item>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Item 1",
                    Quantidade = 10,
                    Preco = 10M
                }
            });
        }
    }
}
