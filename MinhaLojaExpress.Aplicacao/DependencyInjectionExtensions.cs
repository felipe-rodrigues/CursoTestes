using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MinhaLojaExpress.Aplicacao.Common.Pipeline;

namespace MinhaLojaExpress.Aplicacao
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAplicacao(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipe<,>));

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
            
            return services;
        }
    }
}
