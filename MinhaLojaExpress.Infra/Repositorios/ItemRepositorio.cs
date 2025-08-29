using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;
using Microsoft.EntityFrameworkCore;

namespace MinhaLojaExpress.Infra.Repositorios
{
    public class ItemRepositorio(MinhaLojaExpressContext context) : BasicRepositorio<Item>(context), IItemRepositorio
    {
        public async Task<IEnumerable<string>> ListarExistentes(IEnumerable<string> itemsId)
        {
            var ids = itemsId.ToList();
            var res = await context.Items.AsNoTracking()
                .Where(i => ids.Contains(i.Id.ToString()))
                .Select(i => i.Id)
                .ToListAsync();
            return res.Select(id => id.ToString());
        }
    }
}