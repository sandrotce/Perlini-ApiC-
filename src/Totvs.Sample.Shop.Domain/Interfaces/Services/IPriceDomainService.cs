using Totvs.Sample.Shop.Domain.Entities;
using System.Threading.Tasks;
using Tnf.Domain.Services;

namespace Totvs.Sample.Shop.Domain.Interfaces.Services
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IPriceDomainService : IDomainService
    {
        Task<Price> InsertPriceAsync(Price.IPriceBuilder builder);

        Task<Price> UpdatePriceAsync(Price.IPriceBuilder builder);
    }
}
