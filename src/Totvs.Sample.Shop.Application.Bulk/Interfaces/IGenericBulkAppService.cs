using System.Collections.Generic;
using System.Threading.Tasks;
using Totvs.Sample.Shop.Application.Bulk.Services;
using Totvs.Sample.Shop.Dto.BulkResponse;

namespace Totvs.Sample.Shop.Application.Bulk.Interfaces
{
    public interface IGenericBulkAppService<StandardMessageDto, SomeDto>
    {
        Task< (int httpStatus, List<BulkResponseItemDto> bulkResponseList) > UpsertBulk (
            List<StandardMessageDto> businessObjList, 
            string endpoint, 
            DtoConversor<SomeDto, StandardMessageDto> dtoConstructorFunction, 
            UpsertCommand<SomeDto> upsertSomething);
    }
}