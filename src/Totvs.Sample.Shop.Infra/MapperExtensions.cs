using Totvs.Sample.Shop.Infra.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace Totvs.Sample.Shop.Infra
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapperDependency(this IServiceCollection services)
        {
            // Configura o uso do AutoMappper
            return services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<ShopProfile>();
            });
        }
    }
}
