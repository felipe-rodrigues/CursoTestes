using Microsoft.EntityFrameworkCore;
using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Infra.Contexto.Configurations;

namespace MinhaLojaExpress.Infra.Contexto
{
    public class MinhaLojaExpressContext : DbContext
    {
        public MinhaLojaExpressContext(DbContextOptions<MinhaLojaExpressContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemPedido> ItemsPedido { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Desconto> Descontos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new DescontoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new ItemPedidoConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
