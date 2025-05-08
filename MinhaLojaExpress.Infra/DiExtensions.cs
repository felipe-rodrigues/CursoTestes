using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhaLojaExpress.Infra.Contexto;

namespace MinhaLojaExpress.Infra
{
    public static class DiExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MinhaLojaExpressContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5433;Database=MinhaLojaExpressDB;Username=postgres;Password=senha123"));

            return services;
        }
    }
}
