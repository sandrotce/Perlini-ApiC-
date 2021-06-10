using Microsoft.Extensions.DependencyInjection;
using Totvs.Sample.Shop.Application.Directors;
using Totvs.Sample.Shop.Application.Services;
using Totvs.Sample.Shop.Application.Services.Interfaces;
using Totvs.Sample.Shop.Domain;

namespace Totvs.Sample.Shop.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services)
        {
            // Dependência do projeto Security.Domain
            services.AddDomainDependency();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Registro dos serviços
            services.AddTransient<IProductCreator, ProductCreator>();
            services.AddTransient<IPriceCreator, PriceCreator>();
            services.AddTransient(typeof(IGenericAppService<>), typeof(GenericAppService<>));

            return services;
        }
    }
}