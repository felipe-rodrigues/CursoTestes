using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MinhaLojaExpress.Infra.Repositorios
{
    public class DescontoRepositorio(MinhaLojaExpressContext context)
        : BasicRepositorio<Desconto>(context), IDescontoRepositorio
    {
        public override async Task<IEnumerable<Desconto>> GetAllAsync()
        {
            return await context.Descontos.Include(d => d.Items)
                .ToListAsync();
        }
    }
}
