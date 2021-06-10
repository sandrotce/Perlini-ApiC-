using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Product;
using Totvs.Sample.Shop.Infra.Context;
using Totvs.Sample.Shop.Infra.ReadInterfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Totvs.Sample.Shop.Infra.Repositories.ReadRepositories
{
    public class ProductReadRepository : EfCoreRepositoryBase<CrudDbContext, Product>, IProductReadRepository
    {
        public ProductReadRepository(IDbContextProvider<CrudDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<IListDto<ProductResponseDto>> GetAllProductsAsync(ProductRequestAllDto key)
        {
            var response = new ListDto<ProductResponseDto>();

            var query = Context.Products
                .Select(key);

            if (key.ProductActive.HasValue)
                query = query.Where(w => w.IsActive == key.ProductActive.Value);

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
                            ProductResponseDto ProductResponseDto = entity.MapTo<ProductResponseDto>();

                            response.Items.Add(ProductResponseDto);
                        }
                    }
                }
            }

            return response;
        }

        public async Task<ProductResponseDto> GetProductAsync(DefaultRequestDto key)
        {
            var entity = await GetAsync(key);

            return entity.MapTo<ProductResponseDto>();
        }
    }
}
