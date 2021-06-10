using Totvs.Sample.Shop.Web.Controllers;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Totvs.Sample.Shop.Application.Services.Interfaces;
using Tnf.Dto;
using Totvs.Sample.Shop.Dto.Product;
using Totvs.Sample.Shop.Web.Tests.Mocks;
using System.Net;
using Totvs.Sample.Shop.Application.Single.Interfaces;

namespace Totvs.Sample.Shop.Web.Tests.ProductByTestBase
{
    public class ProductControllerTests : TnfAspNetCoreIntegratedTestBase<StartupControllerTest>
    {

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<ProductController>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductAppService>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProductDto>>(
                WebConstants.ProductRouteName
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(3, response.Items.Count);
        }
    }
}
