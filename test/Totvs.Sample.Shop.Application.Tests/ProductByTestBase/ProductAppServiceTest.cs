using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using Tnf.Notifications;
using Tnf.TestBase;
using Totvs.Sample.Shop.Dto.Product;
using Totvs.Sample.Shop.Domain;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Application.Single.Services;
using Totvs.Sample.Shop.Infra;

namespace Totvs.Sample.Shop.Application.Tests.ProductByTestBase
{
    public class ProductAppServiceTests : TnfIntegratedTestBase
    {
        private readonly IProductAppService productAppService;
        protected readonly INotificationHandler notification;
        private readonly string productCode;

        public ProductAppServiceTests()
        {
            this.productAppService = Resolve<IProductAppService>();
            this.notification = Resolve<INotificationHandler>();
            this.productCode = new Random().Next(1, 1000).ToString();
        }

        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            services.AddTransient<IProductAppService, ProductAppService>();
            services.AddTransient<IPriceAppService, PriceAppService>();

            services.AddDomainDependency();
            services.AddInfraSqliteDependency();

            services.BuildServiceProvider();
        }

        protected override void PostInitialize(IServiceProvider provider)
        {
            base.PostInitialize(provider);

            MigrationExtensions.MigrateDatabase(provider);
        }

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<IProductAppService>().ShouldNotBeNull();
        }

        [Fact]
        public async Task Upsert_Product()
        {
            ProductDto productDto = new ProductDto()
            {

                Code = productCode,
                Name = "Product Test " + productCode,
                IsActive = true
            };

            //insert
            var (httpStatus, businessObj) = await productAppService.Upsert(productDto);

            Assert.Equal(productDto.Code, ((ProductResponseDto)businessObj).Code);
            Assert.Equal(productDto.Name, ((ProductResponseDto)businessObj).Name);
            Assert.Equal(productDto.IsActive, ((ProductResponseDto)businessObj).IsActive);

            productDto.Name = "Product Test Update " + productDto.Code;
            productDto.IsActive = false;

            //update
            var resultUpdate = await productAppService.Upsert(productDto);

            Assert.Equal(productDto.Code, ((ProductResponseDto)resultUpdate.businessObj).Code);
            Assert.Equal(productDto.Name, ((ProductResponseDto)resultUpdate.businessObj).Name);
            Assert.Equal(productDto.IsActive, ((ProductResponseDto)resultUpdate.businessObj).IsActive);
        }

        [Fact]
        public async Task GetAll_Product()
        {
            var result = await productAppService.GetAllProductAsync(new ProductRequestAllDto());

            Assert.NotNull(result);
        }
    }
}
