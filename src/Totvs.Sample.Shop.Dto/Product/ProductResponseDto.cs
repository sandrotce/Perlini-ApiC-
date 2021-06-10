using System;

namespace Totvs.Sample.Shop.Dto.Product
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastChange { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
