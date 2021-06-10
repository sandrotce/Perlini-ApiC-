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
    public class ProductRepository : EfCoreRepositoryBase<CrudDbContext, Product>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<CrudDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<Product> InsertProductAndGetIdAsync(Product Product)
            => await InsertAndSaveChangesAsync(Product);

        public async Task<Product> UpdateProductAsync(Product Product)
            => await UpdateAsync(Product);

        public async Task<Product> GetProductByCode(string code)
        {
            var entity = await Context.Products
                .AsNoTracking()
                .Where(p => p.Code == code)
                .FirstOrDefaultAsync();

            return entity.MapTo<Product>();
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var entity = await Context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            return entity.MapTo<Product>();
        }
    }
}
