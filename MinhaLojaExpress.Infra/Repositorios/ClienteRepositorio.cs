using MinhaLojaExpress.Dominio.Entidades;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;

namespace MinhaLojaExpress.Infra.Repositorios
{
    public class ClienteRepositorio : BasicRepositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(MinhaLojaExpressContext context) : base(context)
        {
        }
    }
}
