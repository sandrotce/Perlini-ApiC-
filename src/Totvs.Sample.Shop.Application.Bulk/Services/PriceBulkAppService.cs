using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Notifications;
using Totvs.Sample.Shop.Application.Bulk.Interfaces;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Dto.BulkResponse;
using Totvs.Sample.Shop.Dto.Price;

namespace Totvs.Sample.Shop.Application.Bulk.Services
{
    public class PriceBulkAppService : BulkAppService<PriceDto, PriceBulkDto>, IPriceBulkAppService
    {
        private readonly IPriceAppService appService;
        private readonly INotificationHandler notificationHandler;
        private readonly IGenericBulkAppService<PriceBulkDto, PriceDto> genericBulkAppService;

        public PriceBulkAppService (INotificationHandler notificationHandler, 
        IPriceAppService appService, 
        IGenericBulkAppService<PriceBulkDto, PriceDto> genericBulkAppService) : base (notificationHandler, appService)
        {
            this.appService = appService;
            this.notificationHandler = notificationHandler;
            this.genericBulkAppService = genericBulkAppService;
        }

        public override async Task< (int httpStatus, List<BulkResponseItemDto> bulkResponseList)> UpsertBulk (List<PriceBulkDto> priceList)
        {
            return await genericBulkAppService.UpsertBulk(priceList, "Prices", this.ConvertStandardMessageDto, appService.Upsert);
        }      

        private PriceDto ConvertStandardMessageDto (PriceBulkDto PriceBulkDto)
        {
            return new PriceDto (PriceBulkDto);
        }
    }
}