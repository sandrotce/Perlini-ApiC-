using System;
using Tnf.Notifications;
using Totvs.Sample.Shop.Application.Services.Interfaces;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Dto.Product;

namespace Totvs.Sample.Shop.Application.Directors {
    public class ProductCreator: IProductCreator
    {
        private readonly INotificationHandler notificationHandler;

        public ProductCreator (INotificationHandler notification)
        {
            this.notificationHandler = notification;
        }

        public Product.IProductBuilder ConstructNew (ProductDto dto)
        {
            return Product.Create(notificationHandler)
                .WithCreateDate(DateTime.Now)
                .WithLastChange(DateTime.Now)
                .WithCode(dto.Code)
                .WithName(dto.Name)
                .WithIsActive(dto.IsActive);
        }

        public Product.IProductBuilder ConstructExisting (ProductDto dto, Product entityDomain)
        {
            return Product.Create (notificationHandler, entityDomain)
                .WithLastChange(DateTime.Now)
                .WithCode(dto.Code)
                .WithName(dto.Name)
                .WithIsActive(dto.IsActive);
        }
    }
}