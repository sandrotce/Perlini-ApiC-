using Totvs.Sample.Shop.Domain.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Repositories;

namespace Totvs.Sample.Shop.Domain.Interfaces.Repositories
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IProductRepository : IRepository
    {
        Task<Product> InsertProductAndGetIdAsync(Product Product);
        Task<Product> UpdateProductAsync(Product Product);
        Task<Product> GetProductByCode(string code);
        Task<Product> GetProduct(Guid id);
    }
}
