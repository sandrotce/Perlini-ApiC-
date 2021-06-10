using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Product;

namespace Totvs.Sample.Shop.Web.Controllers
{
    [Route(WebConstants.ProductRouteName)]
    public class ProductController : TnfController
    {
        private readonly IProductAppService _appService;
        private const string _name = "Product";

        public ProductController (IProductAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all product
        /// </summary>
        /// <param name="requestDto">Request params</param>
        /// <returns>List of product</returns>
        [HttpGet]
        [ProducesResponseType (typeof (IListDto<ProductDto>), 200)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> GetAll ([FromQuery] ProductRequestAllDto requestDto)
        {
            var response = await _appService.GetAllProductAsync (requestDto);

            return CreateResponseOnGetAll (response, _name);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="requestDto">Request params</param>
        /// <returns>Product requested</returns>
        [HttpGet ("{id}")]
        [ProducesResponseType (typeof (ProductDto), 200)]
        [ProducesResponseType (404)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> Get (Guid id, [FromQuery] RequestDto requestDto)
        {
            var request = new DefaultRequestDto (id, requestDto);

            var response = await _appService.GetProductAsync (request);

            return CreateResponseOnGet (response, _name);
        }

        /// <summary>
        /// Create or update a product
        /// </summary>
        /// <param name="productDto">Product to create or update</param>
        /// <returns>Product created or updated</returns>
        [HttpPost]
        [ProducesResponseType (typeof (ProductResponseDto), 200)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> Post ([FromBody] ProductDto productDto)
        {            
            var appServiceResponse = await _appService.Upsert(productDto);
            return StatusCode (appServiceResponse.httpStatus, appServiceResponse.businessObj);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="productDto">Product content to update</param>
        /// <returns>Updated product</returns>
        [HttpPut ("{id}")]
        [ProducesResponseType (typeof (ProductDto), 200)]
        [ProducesResponseType (typeof (ErrorResponse), 400)]
        public async Task<IActionResult> Put (Guid id, [FromBody] ProductDto productDto)
        {
            productDto = await _appService.UpdateProductAsync(id, productDto);

            return CreateResponseOnPut (productDto, _name);
        }
    }
}