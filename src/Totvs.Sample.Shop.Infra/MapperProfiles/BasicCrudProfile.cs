using AutoMapper;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Dto.Product;
using Totvs.Sample.Shop.Dto.Price;

namespace Totvs.Sample.Shop.Infra.MapperProfiles
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductResponseDto>();

            CreateMap<Price, PriceDto>();
            CreateMap<Price, PriceResponseDto>();
        }
    }
}
