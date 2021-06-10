using System;
using System.Collections.Generic;
using Tnf.Dto;

namespace Totvs.Sample.Shop.Dto.Price
{
    public class PriceBulkDto : BaseDto
    {
        public string ProductCode { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Value { get; set; }
    }
}