using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Tnf.AspNetCore.Mvc.Response;
using Totvs.Sample.Shop.Application.Bulk.Interfaces;
using Totvs.Sample.Shop.Dto.Price;

namespace Totvs.Sample.Shop.Web.Controllers.Bulk
{
    [Route(WebConstants.PriceBulkRouteName)]
    public class PriceBulkController : BulkController<PriceBulkDto>
    {
        private readonly IPriceBulkAppService _appService;
        private const string _name = "Price";
        public PriceBulkController (IPriceBulkAppService appService): base(appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Create or update bulk price
        /// </summary>
        /// <param name="priceList">List of price to create or update</param>
        /// <returns>Price items created or updated</returns>
        [HttpPost]
        [ProducesResponseType (200)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> Post ([FromBody] List<PriceBulkDto> priceList)
        {
            using (LogContext.PushProperty("HttpContextId", HttpContext.TraceIdentifier))
            {
                var response = await _appService.UpsertBulk(priceList);
                return StatusCode(response.httpStatus, response.bulkResponseList);
            }
        }
    }
}