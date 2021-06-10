using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Tnf.AspNetCore.Mvc.Response;
using Totvs.Sample.Shop.Application.Bulk.Interfaces;
using Totvs.Sample.Shop.Dto.Product;

namespace Totvs.Sample.Shop.Web.Controllers.Bulk
{
    [Route(WebConstants.ProductBulkRouteName)]
    public class ProductBulkController : BulkController<ProductBulkDto>
    {
        private readonly IProductBulkAppService _appService;
        private const string _name = "Product";
        public ProductBulkController (IProductBulkAppService appService): base(appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Create or update bulk product
        /// </summary>
        /// <param name="productList">List of product to create or update</param>
        /// <returns>Product created or updated</returns>
        [HttpPost]
        [ProducesResponseType (200)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> Post ([FromBody] List<ProductBulkDto> productList)
        {
            using (LogContext.PushProperty("HttpContextId", HttpContext.TraceIdentifier))
            {
                var response = await _appService.UpsertBulk(productList);
                return StatusCode(response.httpStatus, response.bulkResponseList);
            }
        }
    }
}