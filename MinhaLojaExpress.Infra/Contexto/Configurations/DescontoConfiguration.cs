using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Infra.Contexto.Configurations
{
    public class DescontoConfiguration : IEntityTypeConfiguration<Desconto>
    {
        public void Configure(EntityTypeBuilder<Desconto> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.Percentual)
                .IsRequired();

            builder.Property(d => d.DataValidade)
                .IsRequired();

            builder.HasMany(d => d.Items)
                .WithMany(i => i.Descontos)
                .UsingEntity<Dictionary<string, object>>(
                    "DescontoItem",
                    j => j.HasOne<Item>().WithMany().HasForeignKey("ItemId"),
                    j => j.HasOne<Desconto>().WithMany().HasForeignKey("DescontoId")
                );
        }
    }
}
