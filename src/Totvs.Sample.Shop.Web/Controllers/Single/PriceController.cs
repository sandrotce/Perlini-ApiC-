using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Price;

namespace Totvs.Sample.Shop.Web.Controllers
{
    [Route(WebConstants.PriceRouteName)]
    public class PriceController : TnfController
    {
        private readonly IPriceAppService _appService;
        private const string _name = "Price";

        public PriceController (IPriceAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all price
        /// </summary>
        /// <param name="requestDto">Request params</param>
        /// <returns>List of price</returns>
        [HttpGet]
        [ProducesResponseType (typeof (IListDto<PriceDto>), 200)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> GetAll ([FromQuery] PriceRequestAllDto requestDto)
        {
            var response = await _appService.GetAllPriceAsync (requestDto);

            return CreateResponseOnGetAll (response, _name);
        }

        /// <summary>
        /// Get price by id
        /// </summary>
        /// <param name="id">Price id</param>
        /// <param name="requestDto">Request params</param>
        /// <returns>Price requested</returns>
        [HttpGet ("{id}")]
        [ProducesResponseType (typeof (PriceDto), 200)]
        [ProducesResponseType (404)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> Get (Guid id, [FromQuery] RequestDto requestDto)
        {
            var request = new DefaultRequestDto (id, requestDto);

            var response = await _appService.GetPriceAsync (request);

            return CreateResponseOnGet (response, _name);
        }

        /// <summary>
        /// Create or update a price
        /// </summary>
        /// <param name="priceDto">Price to create</param>
        /// <returns>Price created or updated</returns>
        [HttpPost]
        [ProducesResponseType (typeof (PriceResponseDto), 200)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> Post ([FromBody] PriceDto priceDto)
        {
            var appServiceResponse = await _appService.Upsert(priceDto);
            return StatusCode(appServiceResponse.httpStatus, appServiceResponse.businessObj);
        }

        /// <summary>
        /// Update a price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <param name="priceDto">Price content to update</param>
        /// <returns>Updated price</returns>
        [HttpPut ("{id}")]
        [ProducesResponseType (typeof (PriceDto), 200)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> Put (Guid id, [FromBody] PriceDto priceDto)
        {
            priceDto = await _appService.UpdatePriceAsync (id, priceDto);

            return CreateResponseOnPut (priceDto, _name);
        }
    }
}