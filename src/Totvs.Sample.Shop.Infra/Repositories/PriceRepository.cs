using Microsoft.EntityFrameworkCore;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Domain.Interfaces.Repositories;
using Totvs.Sample.Shop.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Totvs.Sample.Shop.Infra
{
    public class PriceRepository : EfCoreRepositoryBase<CrudDbContext, Price>, IPriceRepository
    {
        public PriceRepository(IDbContextProvider<CrudDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<Price> InsertPriceAndGetIdAsync(Price Price)
            => await InsertAndSaveChangesAsync(Price);

        public async Task<Price> UpdatePriceAsync(Price Price)
        {
            Context.Entry(Price).State = EntityState.Modified;

            return await UpdateAsync(Price);
        }

        public async Task<Price> GetPriceByProductCode(string productCode)
        {
            var entity = await Context.Prices
                .AsNoTracking()
                .Include(p => p.Product)
                .Where(p => p.Product.Code == productCode)
                .FirstOrDefaultAsync();

            return entity.MapTo<Price>();
        }

        public async Task<Price> GetPrice(Guid id)
        {
            var entity = await Context.Prices
                .AsNoTracking()
                .Include(p => p.Product)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            return entity.MapTo<Price>();
        }
    }
}
