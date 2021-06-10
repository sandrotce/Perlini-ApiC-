using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Notifications;
using Totvs.Sample.Shop.Dto.Product;
using Totvs.Sample.Shop.Application.Bulk.Interfaces;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Dto.BulkResponse;

namespace Totvs.Sample.Shop.Application.Bulk.Services
{
    public class ProductBulkAppService : BulkAppService<ProductDto, ProductBulkDto>, IProductBulkAppService
    {
        private readonly IProductAppService appService;
        private readonly INotificationHandler notificationHandler;
        private readonly IGenericBulkAppService<ProductBulkDto, ProductDto> genericBulkAppService;

        public ProductBulkAppService(INotificationHandler notificationHandler,
        IProductAppService appService,
        IGenericBulkAppService<ProductBulkDto, ProductDto> genericBulkAppService) : base(notificationHandler, appService)
        {
            this.appService = appService;
            this.notificationHandler = notificationHandler;
            this.genericBulkAppService = genericBulkAppService;
        }

        public override async Task<(int httpStatus, List<BulkResponseItemDto> bulkResponseList)> UpsertBulk(List<ProductBulkDto> productList)
        {
            return await genericBulkAppService.UpsertBulk(productList, "Products", this.ConvertStandardMessageDto, appService.Upsert);
        }

        private ProductDto ConvertStandardMessageDto(ProductBulkDto ProductBulkDto)
        {
            return new ProductDto(ProductBulkDto);
        }
    }
}