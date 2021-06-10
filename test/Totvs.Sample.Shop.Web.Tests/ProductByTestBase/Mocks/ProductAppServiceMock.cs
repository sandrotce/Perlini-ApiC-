using Totvs.Sample.Shop.Application.Services.Interfaces;
using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Dto;
using Totvs.Sample.Shop.Application.Single.Interfaces;

namespace Totvs.Sample.Shop.Web.Tests.Mocks
{
    public class ProductAppServiceMock : IProductAppService
    {
        public static string productCode = "123";

        Task<ProductResponseDto> IProductAppService.GetProductAsync(DefaultRequestDto id)
        {
            throw new NotImplementedException();
        }

        Task<IListDto<ProductResponseDto>> IProductAppService.GetAllProductAsync(ProductRequestAllDto request)
        {
            var list = new List<ProductResponseDto>()
            {
                new ProductResponseDto() { Code = "123", Name = "Product A", IsActive = true },
                new ProductResponseDto() { Code = "456", Name = "Product B", IsActive = true },
                new ProductResponseDto() { Code = "789", Name = "Product C",IsActive = true }
            };

            IListDto<ProductResponseDto> result = new ListDto<ProductResponseDto> { HasNext = false, Items = list };

            return result.AsTask();
        }

        public Task<(int httpStatus, dynamic businessObj)> Upsert(ProductDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> UpdateProductAsync(Guid id, ProductDto Product)
        {
            throw new NotImplementedException();
        }
    }
}
