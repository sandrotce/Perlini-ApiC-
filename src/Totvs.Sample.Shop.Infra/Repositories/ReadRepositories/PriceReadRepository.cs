using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Price;
using Totvs.Sample.Shop.Infra.Context;
using Totvs.Sample.Shop.Infra.ReadInterfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Totvs.Sample.Shop.Infra.Repositories.ReadRepositories
{
    public class PriceReadRepository : EfCoreRepositoryBase<CrudDbContext, Price>, IPriceReadRepository
    {
        public PriceReadRepository(IDbContextProvider<CrudDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<IListDto<PriceResponseDto>> GetAllPricesAsync(PriceRequestAllDto key)
        {
            var response = new ListDto<PriceResponseDto>();

            var query = Context.Prices
                .Include(i => i.Product)
                .Select(key);

            if (key.PriceActive.HasValue)
                query = query.Where(w => w.IsActive == key.PriceActive.Value);

            var listEntity = await query.ToListDtoAsync(key);

            if (listEntity != null)
            {
                response.HasNext = listEntity.HasNext;

                if (listEntity.Items != null && listEntity.Items.Count > 0)
                {
                    foreach (var entity in listEntity.Items)
                    {
                        if (entity != null)
                        {
                            PriceResponseDto priceResponseDto = entity.MapTo<PriceResponseDto>();

                            if (entity.Product != null)
                            {
                                priceResponseDto.ProductCode = entity.Product.Code;
                                priceResponseDto.ProductName = entity.Product.Name;
                            }

                            response.Items.Add(priceResponseDto);
                        }
                    }
                }
            }
            return response;
        }

        public async Task<PriceResponseDto> GetPriceAsync(DefaultRequestDto key)
        {
            var entity = await Context.Prices
               .Include(i => i.Product)
               .Where(w => w.Id == key.Id)
               .Select(key)
               .FirstOrDefaultAsync();

            return entity.MapTo<PriceResponseDto>();
        }
    }
}
