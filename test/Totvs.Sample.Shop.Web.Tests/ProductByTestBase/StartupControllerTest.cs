using Totvs.Sample.Shop.Application.Services.Interfaces;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Web.Tests.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tnf.Application.Services;
using Totvs.Sample.Shop.Application.Single.Services;

namespace Totvs.Sample.Shop.Web.Tests.ProductByTestBase
{
    public class StartupControllerTest
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Configura o setup de teste para AspNetCore
            services.AddTnfAspNetCoreSetupTest();

            // Registro dos serviços de Mock
            services.AddTransient<IProductAppService, ProductAppServiceMock>();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
