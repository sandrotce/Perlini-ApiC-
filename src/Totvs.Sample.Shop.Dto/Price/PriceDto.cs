using System;
using Tnf.Dto;

namespace Totvs.Sample.Shop.Dto.Price
{
    public class PriceDto : BaseDto
    {
        public PriceDto(PriceBulkDto priceBulkDto)
        {
            this.ProductCode = priceBulkDto.ProductCode;
            this.StartDate = priceBulkDto.StartDate;
            this.EndDate = priceBulkDto.EndDate;
            this.IsActive = priceBulkDto.IsActive;
            this.Value = priceBulkDto.Value;
        }

        public PriceDto()
        {

        }
        public string ProductCode { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Value { get; set; }
    }

}