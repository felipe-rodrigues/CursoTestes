using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;

namespace MinhaLojaExpress.Infra.Repositorios
{
    public class PedidoRepositorio : BasicRepositorio<Pedido>, IPedidoRepositorio
    {
        public PedidoRepositorio(MinhaLojaExpressContext context) : base(context)
        {
        }
    }
}
