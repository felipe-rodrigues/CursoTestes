using Microsoft.EntityFrameworkCore;
using MinhaLojaExpress.Infra.Contexto;

namespace MinhaLojaExpress.Testes.Unit.Infra.Repositories
{
    public class Database 
    {
        public MinhaLojaExpressContext Context { get; set; }
        public string DataBaseName { get; }
        
        public Database()
        {
            DataBaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<MinhaLojaExpressContext>()
                .UseInMemoryDatabase(DataBaseName)
                .Options;
            Context = new MinhaLojaExpressContext(options);
        }
    }
}
