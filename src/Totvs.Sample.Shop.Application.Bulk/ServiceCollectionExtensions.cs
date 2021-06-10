using Microsoft.Extensions.DependencyInjection;
using Totvs.Sample.Shop.Application.Bulk.Interfaces;
using Totvs.Sample.Shop.Application.Bulk.Services;
using Totvs.Sample.Shop.Domain;

namespace Totvs.Sample.Shop.Application.Bulk
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBulkApplicationServiceDependency (this IServiceCollection services)
        {
            // Dependência do projeto Security.Domain
            services.AddDomainDependency ();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Registro dos serviços            
            services.AddTransient<IPriceBulkAppService, PriceBulkAppService> ();
            services.AddTransient<IProductBulkAppService, ProductBulkAppService> ();
            services.AddTransient(typeof(IGenericBulkAppService<,>), typeof(GenericBulkAppService<,>));

            return services;
        }
    }
}