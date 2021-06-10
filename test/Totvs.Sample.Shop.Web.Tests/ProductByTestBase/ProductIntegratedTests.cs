using Totvs.Sample.Shop.Application.Services.Interfaces;
using Totvs.Sample.Shop.Domain;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Domain.Interfaces.Repositories;
using Totvs.Sample.Shop.Domain.Interfaces.Services;
using Totvs.Sample.Shop.Dto.Product;
using Totvs.Sample.Shop.Infra.Context;
using Totvs.Sample.Shop.Infra.ReadInterfaces;
using Totvs.Sample.Shop.Web.Controllers;
using Totvs.Sample.Shop.Web.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tnf;
using Tnf.Application.Services;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.AspNetCore.TestBase;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.Notifications;
using Xunit;
using Totvs.Sample.Shop.Application.Single.Interfaces;

namespace Totvs.Sample.Shop.Web.Tests.ProductByTestBase
{
    public class ProductIntegratedTests : TnfAspNetCoreIntegratedTestBase<StartupIntegratedTest>
    {
        public ProductIntegratedTests()
        {
            var notificationHandler = new NotificationHandler(ServiceProvider);

            SetRequestCulture(CultureInfo.GetCultureInfo("pt-BR"));

            ServiceProvider.UsingDbContext<CrudDbContext>(context =>
            {
                context.Products.Add(Product.Create(notificationHandler)
                    .WithCode(ProductAppServiceMock.productCode)
                    .WithName("Product A")
                    .WithIsActive(true)
                    .Build());

                for (var i = 2; i < 21; i++)
                    context.Products.Add(Product.Create(notificationHandler)
                        .WithCode(new Random().Next(1, 1000).ToString())
                        .WithName($"Product {NumberToAlphabetLetter(i, true)}")
                        .WithIsActive(true)
                        .Build());

                context.SaveChanges();
            });
        }

        private string NumberToAlphabetLetter(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<ProductController>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductAppService>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductDomainService>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductRepository>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductReadRepository>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProductDto>>(
                WebConstants.ProductRouteName
            );

            // Assert
            Assert.True(response.HasNext);
            Assert.Equal(10, response.Items.Count);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<ProductDto>>(
                $"{WebConstants.ProductRouteName}?pageSize=30"
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(20, response.Items.Count);
        }
    }
}
