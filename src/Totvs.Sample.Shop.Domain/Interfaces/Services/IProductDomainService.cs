using Totvs.Sample.Shop.Domain.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;

namespace Totvs.Sample.Shop.Domain.Interfaces.Services
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IProductDomainService : IDomainService
    {
        Task<Product> InsertProductAsync(Product.IProductBuilder builder);

        Task<Product> UpdateProductAsync(Product.IProductBuilder builder);
    }
}
