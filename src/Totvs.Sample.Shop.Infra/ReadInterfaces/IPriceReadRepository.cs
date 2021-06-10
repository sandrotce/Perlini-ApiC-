using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Price;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace Totvs.Sample.Shop.Infra.ReadInterfaces
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IPriceReadRepository : IRepository
    {
        Task<PriceResponseDto> GetPriceAsync(DefaultRequestDto key);

        Task<IListDto<PriceResponseDto>> GetAllPricesAsync(PriceRequestAllDto key);
    }
}
