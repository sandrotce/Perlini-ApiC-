using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Product;
using System;
using System.Threading.Tasks;
using Tnf.Dto;

namespace Totvs.Sample.Shop.Application.Single.Interfaces
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IProductAppService : IBaseSingleInterface<ProductDto>
    {
        Task<ProductDto> UpdateProductAsync(Guid id, ProductDto Product);
        Task<ProductResponseDto> GetProductAsync(DefaultRequestDto id);
        Task<IListDto<ProductResponseDto>> GetAllProductAsync(ProductRequestAllDto request);
    }
}
