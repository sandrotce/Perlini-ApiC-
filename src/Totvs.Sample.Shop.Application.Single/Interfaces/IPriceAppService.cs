using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Price;
using System;
using System.Threading.Tasks;
using Tnf.Dto;

namespace Totvs.Sample.Shop.Application.Single.Interfaces
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IPriceAppService : IBaseSingleInterface<PriceDto>
    {
        Task<PriceDto> UpdatePriceAsync(Guid id, PriceDto Price);
        Task<PriceResponseDto> GetPriceAsync(DefaultRequestDto id);
        Task<IListDto<PriceResponseDto>> GetAllPriceAsync(PriceRequestAllDto request);
    }
}
