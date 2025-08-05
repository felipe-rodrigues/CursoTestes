using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MinhaLojaExpress.Infra.Contexto
{
    public class MinhaLojaExpressContextFactory : IDesignTimeDbContextFactory<MinhaLojaExpressContext>
    {
        public MinhaLojaExpressContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("PostgresConnectionString");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("A variável de ambiente PostgresConnectionString nao foi encontrada");
            }

            var optionsBuilder = new DbContextOptionsBuilder<MinhaLojaExpressContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new MinhaLojaExpressContext(optionsBuilder.Options);
        }
    }
}
