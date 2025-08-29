using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;


namespace MinhaLojaExpress.Infra.Contexto
{
    public class InicializadorBancoDeDados(MinhaLojaExpressContext context)
    {
        public async Task InicializarAsync()
        {
        }
    }
}
