using Totvs.Sample.Shop.Domain;
using Totvs.Sample.Shop.Infra.Context;
using Totvs.Sample.Shop.Infra.SqLite.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Totvs.Sample.Shop.Infra.SqLite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqLiteDependency(this IServiceCollection services)
        {
            services
                .AddInfraDependency()
                .AddTnfDbContext<CrudDbContext, SqliteCrudDbContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                    {
                        config.DbContextOptions.EnableSensitiveDataLogging();
                        config.DbContextOptions.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                        config.UseLoggerFactory();
                    }

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlite(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlite(config.ConnectionString);
                });

            return services;
        }
    }
}
