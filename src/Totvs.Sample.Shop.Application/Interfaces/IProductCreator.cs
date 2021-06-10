using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Dto.Product;

namespace Totvs.Sample.Shop.Application.Services.Interfaces
{
    public interface IProductCreator
    {
        Product.IProductBuilder ConstructNew (ProductDto dto);
        Product.IProductBuilder ConstructExisting (ProductDto dto, Product entityDomain);
    }
}