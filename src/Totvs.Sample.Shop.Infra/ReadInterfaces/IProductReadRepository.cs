using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Product;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace Totvs.Sample.Shop.Infra.ReadInterfaces
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IProductReadRepository : IRepository
    {
        Task<ProductResponseDto> GetProductAsync(DefaultRequestDto key);

        Task<IListDto<ProductResponseDto>> GetAllProductsAsync(ProductRequestAllDto key);
    }
}
