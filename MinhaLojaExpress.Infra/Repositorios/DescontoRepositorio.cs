using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;

namespace MinhaLojaExpress.Infra.Repositorios
{
    public class DescontoRepositorio : BasicRepositorio<Desconto>, IDescontoRepositorio
    {
        public DescontoRepositorio(MinhaLojaExpressContext context) : base(context)
        {
        }
    }
}
