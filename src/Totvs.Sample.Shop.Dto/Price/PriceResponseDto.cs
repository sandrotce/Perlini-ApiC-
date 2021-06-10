using System;

namespace Totvs.Sample.Shop.Dto.Price
{
    public class PriceResponseDto
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Value { get; set; }
    }
}
