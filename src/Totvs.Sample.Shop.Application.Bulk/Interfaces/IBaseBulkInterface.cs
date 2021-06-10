using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Totvs.Sample.Shop.Dto.BulkResponse;

namespace Totvs.Sample.Shop.Application.Bulk.Interfaces
{
    public interface IBaseBulkInterface<T>
    {
        Task<(int httpStatus, List<BulkResponseItemDto> bulkResponseList)> UpsertBulk (List<T> businessObjList);
    }
}