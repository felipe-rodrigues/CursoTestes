using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;

namespace MinhaLojaExpress.Infra.Repositorios
{
    public class ItemRepositorio : BasicRepositorio<Item>, IItemRepositorio
    {
        public ItemRepositorio(MinhaLojaExpressContext context) : base(context)
        {
        }
    }
}