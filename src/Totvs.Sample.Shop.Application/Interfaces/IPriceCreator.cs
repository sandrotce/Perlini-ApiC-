using System;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Dto.Price;

namespace Totvs.Sample.Shop.Application.Services.Interfaces
{
    public interface IPriceCreator
    {
        Price.IPriceBuilder ConstructNew (PriceDto dto, Guid productId);
        Price.IPriceBuilder ConstructExisting (PriceDto dto, Price entityDomain);
    }
}