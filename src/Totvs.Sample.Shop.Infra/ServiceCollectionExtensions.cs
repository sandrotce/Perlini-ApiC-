using Totvs.Sample.Shop.Domain.Interfaces.Repositories;
using Totvs.Sample.Shop.Infra.ReadInterfaces;
using Totvs.Sample.Shop.Infra.Repositories.ReadRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Totvs.Sample.Shop.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependency(this IServiceCollection services)
        {
            services
                .AddTnfEntityFrameworkCore()    // Configura o uso do EntityFrameworkCore registrando os contextos que serão usados pela aplicação
                .AddMapperDependency();         // Configura o uso do AutoMapper

            // Registro dos repositórios
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductReadRepository, ProductReadRepository>();

            services.AddTransient<IPriceRepository, PriceRepository>();
            services.AddTransient<IPriceReadRepository, PriceReadRepository>();
            
            return services;
        }
    }
}
