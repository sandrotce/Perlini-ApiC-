using Microsoft.Extensions.DependencyInjection;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Application.Single.Services;
using Totvs.Sample.Shop.Domain;

namespace Totvs.Sample.Shop.Application.Single
{
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddSingleApplicationServiceDependency (this IServiceCollection services)
        {
            // Dependência do projeto Security.Domain
            services.AddDomainDependency ();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Registro dos serviços
            services.AddTransient<IProductAppService, ProductAppService>();
            services.AddTransient<IPriceAppService, PriceAppService>();            

            return services;
        }
    }
}