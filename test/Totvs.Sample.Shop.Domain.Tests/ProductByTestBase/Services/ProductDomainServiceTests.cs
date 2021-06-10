using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Domain.Interfaces.Repositories;
using Totvs.Sample.Shop.Domain.Interfaces.Services;
using Totvs.Sample.Shop.Domain.Services;
using Totvs.Sample.Shop.Domain.Tests.ProductByTestBase.Mocks;
using Tnf.TestBase;
using Xunit;
using Moq;
using Tnf.Repositories.Uow;


namespace Totvs.Sample.Shop.Domain.Tests.ProductByTestBase.Services
{
    public class ProductDomainServiceTests : TnfIntegratedTestBase
    {
        private readonly IProductDomainService _domainService;
        private readonly CultureInfo _culture;

        public ProductDomainServiceTests()
        {
            _domainService = Resolve<IProductDomainService>();

            _culture = CultureInfo.GetCultureInfo("pt-BR");
        }

        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            // Registro dos serviços de Mock
            services.AddTransient<IProductRepository, ProductRepositoryMock>();

            // Registro do UoW
            AddUoW(services);

            // Registro dos serviços para teste
            services.AddTransient<IProductDomainService, ProductDomainService>();
        }

        private static void AddUoW(IServiceCollection services)
        {
            var mockIUnitOfWorkManager = new Mock<IUnitOfWorkManager>();

            mockIUnitOfWorkManager
               .Setup(m => m.Begin())
               .Returns(new Mock<IUnitOfWorkCompleteHandle>().Object);

            services.AddSingleton(mockIUnitOfWorkManager.Object);
        }

        protected override void PostInitialize(IServiceProvider provider)
        {
            base.PostInitialize(provider);

            provider.ConfigureTnf().UseDomainLocalization();
        }

        [Fact]
        public void Should_Resolve_All()
        {
            Assert.NotNull(TnfSession);
            Assert.NotNull(ServiceProvider.GetService<IProductDomainService>());
            Assert.NotNull(ServiceProvider.GetService<IProductRepository>());
        }

        [Fact]
        public async Task Should_Create_Product()
        {
            // Act
            var product = await _domainService.InsertProductAsync(
                Product.Create(LocalNotification)
                    .WithCode(new Random().Next(1, 1000).ToString())
                    .WithName("Test Product")
                    .WithIsActive(true));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.NotEqual(Guid.Empty, product.Id);
            Assert.Equal("Test Product", product.Name);
            Assert.True(product.IsActive);
        }


        [Fact]
        public async Task Should_Update_Product()
        {
            // Act
            var product = await _domainService.UpdateProductAsync(
                Product.Create(LocalNotification)
                    .WithCode(ProductRepositoryMock.productCode)
                    .WithName("Test Product")
                    .WithIsActive(true));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.Equal(product.Code, ProductRepositoryMock.productCode);
            Assert.Equal("Test Product", product.Name);
            Assert.True(product.IsActive);
        }

    }
}
