using Totvs.Sample.Shop.Domain.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Repositories;

namespace Totvs.Sample.Shop.Domain.Interfaces.Repositories
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IPriceRepository : IRepository
    {
        Task<Price> InsertPriceAndGetIdAsync(Price Price);
        Task<Price> UpdatePriceAsync(Price Price);
        Task<Price> GetPriceByProductCode(string productCode);
        Task<Price> GetPrice(Guid id);
    }
}
