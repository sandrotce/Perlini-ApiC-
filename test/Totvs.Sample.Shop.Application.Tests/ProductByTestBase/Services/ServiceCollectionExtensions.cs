using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Configuration;
using Totvs.Sample.Shop.Infra;
using Totvs.Sample.Shop.Domain;
using Totvs.Sample.Shop.Infra.Context;
using Totvs.Sample.Shop.Infra.SqLite.Context;

namespace Totvs.Sample.Shop.Application.Tests.ProductByTestBase
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDbContext<TDbContext>(TnfDbContextConfiguration<TDbContext> config)
            where TDbContext : TnfDbContext
        {
            string CONNECTIONSTRING = "Data Source=Totvs.Sample.Shop.db";
            config.DbContextOptions.UseSqlite(CONNECTIONSTRING);     

        }

        public static IServiceCollection AddInfraSqliteDependency(this IServiceCollection services)
        {
            services
                .AddMemoryCache()
                .AddApplicationServiceDependency()
                .AddInfraDependency()
                .AddTnfAspNetCore()
                .AddDomainDependency()
                .AddTnfDbContext<CrudDbContext, SqliteCrudDbContext>(ConfigureDbContext)
                .AddCorsAll("AllowAll")
                .AddTnfNotifications()
                .AddTnfRuntime();

            return services;
        }
    }
}
