using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MinhaLojaExpress.Infra.Contexto
{
    public class MinhaLojaExpressContextFactory : IDesignTimeDbContextFactory<MinhaLojaExpressContext>
    {
        public MinhaLojaExpressContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MinhaLojaExpressContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=MinhaLojaExpressDB;Username=postgres;Password=senha123");

            return new MinhaLojaExpressContext(optionsBuilder.Options);
        }
    }
}
