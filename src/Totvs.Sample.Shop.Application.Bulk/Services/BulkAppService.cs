using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Notifications;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Application.Bulk.Interfaces;
using Totvs.Sample.Shop.Dto.BulkResponse;

namespace Totvs.Sample.Shop.Application.Bulk.Services
{
    public abstract class BulkAppService<Dto, StandardMessageDto>: ApplicationService, IBaseBulkInterface<StandardMessageDto>
    {
        private readonly IBaseSingleInterface<Dto> appService;
        public BulkAppService(INotificationHandler notificationHandler,IBaseSingleInterface<Dto> appService): base(notificationHandler)
        {
            this.appService = appService;
        }

        public abstract Task<(int httpStatus, List<BulkResponseItemDto> bulkResponseList)> UpsertBulk(List<StandardMessageDto> businessObjList);
    }
}