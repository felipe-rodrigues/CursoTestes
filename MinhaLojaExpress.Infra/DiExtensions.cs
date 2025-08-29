using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;
using MinhaLojaExpress.Infra.Repositorios;

namespace MinhaLojaExpress.Infra
{
    public static class DiExtensions
    {
        public static IServiceCollection AddBancoDeDados(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresConnectionString");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("PostgresConnectionString não encontrada");

            services.AddDbContext<MinhaLojaExpressContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<InicializadorBancoDeDados>();

            return services;
        }

        public static IServiceCollection AddRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IItemRepositorio, ItemRepositorio>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IDescontoRepositorio, DescontoRepositorio>();

            return services;
        }

        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBancoDeDados(configuration);
            services.AddRepositorios();

            return services;
        }
    }
}
