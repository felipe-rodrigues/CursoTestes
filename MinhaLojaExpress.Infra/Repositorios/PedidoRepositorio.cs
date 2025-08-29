using Microsoft.EntityFrameworkCore;
using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;

namespace MinhaLojaExpress.Infra.Repositorios
{
    public class PedidoRepositorio(MinhaLojaExpressContext context)
        : BasicRepositorio<Pedido>(context), IPedidoRepositorio
    {
        public override async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await context.Pedidos.Include(p => p.Cliente)
                .Include(p => p.Items)
                .ThenInclude(ip => ip.Item)
                .Include(p => p.Descontos)
                .ToListAsync();
        }
        
        public override async Task<Pedido?> GetByIdAsync(Guid id)
        {
            return await context.Pedidos.Include(p => p.Cliente)
                .Include(p => p.Items)
                .ThenInclude(ip => ip.Item)
                .Include(p => p.Descontos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
