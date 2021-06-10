using System.Threading.Tasks;
using Tnf.Application.Services;

namespace Totvs.Sample.Shop.Application.Single.Interfaces
{
    public interface IBaseSingleInterface<T>
    {
        Task<(int httpStatus, dynamic businessObj)> Upsert(T dto);
    }
}