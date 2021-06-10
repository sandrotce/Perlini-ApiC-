using System;
using Tnf.Dto;

namespace Totvs.Sample.Shop.Dto.Product
{
    public class ProductDto : BaseDto
    {

        public ProductDto (ProductBulkDto productBulkDto)
        {
            this.Code = productBulkDto.Code;
            this.Name = productBulkDto.Name;
            this.IsActive = productBulkDto.IsActive;
        }

        public ProductDto()
        {

        }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
