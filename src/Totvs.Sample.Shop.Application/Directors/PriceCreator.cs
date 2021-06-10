using System;
using Tnf.Notifications;
using Totvs.Sample.Shop.Application.Services.Interfaces;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Dto.Price;

namespace Totvs.Sample.Shop.Application.Directors
{
    public class PriceCreator : IPriceCreator
    {
        private readonly INotificationHandler notificationHandler;

        public PriceCreator (INotificationHandler notification)
        {
            this.notificationHandler = notification;
        }

        public Price.IPriceBuilder ConstructNew (PriceDto dto, Guid productId)
        {
            return Price.Create(notificationHandler)
                    .WithCreateDate(DateTime.Now)
                    .WithLastChange(DateTime.Now)
                    .WithProductId(productId)
                    .WithStartDate(dto.StartDate)
                    .WithEndDate(dto.EndDate)
                    .WithIsActive(dto.IsActive)
                    .WithValue(dto.Value);
        }

        public Price.IPriceBuilder ConstructExisting (PriceDto dto, Price entityDomain)
        {
            return Price.Create (notificationHandler, entityDomain)
                    .WithLastChange(DateTime.Now)
                    .WithStartDate(dto.StartDate)
                    .WithEndDate(dto.EndDate)
                    .WithIsActive(dto.IsActive)
                    .WithValue(dto.Value);
        }
    }
}