using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MinhaLojaExpress.Aplicacao
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAplicacao(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
            
            return services;
        }
    }
}
