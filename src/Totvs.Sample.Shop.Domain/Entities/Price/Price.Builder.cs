using System;
using System.Collections.Generic;
using System.Linq;
using Tnf.Builder;
using Tnf.Notifications;
using Totvs.Sample.Shop.Domain.Entities.Specifications;

namespace Totvs.Sample.Shop.Domain.Entities {
    public partial class Price {
        public class Builder : Builder<Price>, IPriceBuilder
        {
            public Builder(INotificationHandler notificationHandler) : base(notificationHandler) { }

            public Builder(INotificationHandler notificationHandler, Price instance) : base(notificationHandler, instance) { }

            public Builder GenerateNewPrice()
            {
                Instance.Id = Guid.NewGuid();

                return this;
            }

            IPriceBuilder IPriceBuilder.WithCreateDate(DateTimeOffset createDate)
            {
                Instance.CreateDate = createDate;
                return this;
            }

            IPriceBuilder IPriceBuilder.WithLastChange(DateTimeOffset lastChange)
            {
                Instance.LastChange = lastChange;
                return this;
            }

            IPriceBuilder IPriceBuilder.WithProductId(Guid productId)
            {
                Instance.ProductId = productId;
                return this;
            }

            IPriceBuilder IPriceBuilder.WithStartDate(DateTimeOffset startDate)
            {
                Instance.StartDate = startDate;
                return this;
            }

            IPriceBuilder IPriceBuilder.WithEndDate(DateTimeOffset endDate)
            {
                Instance.EndDate = endDate;
                return this;
            }

            IPriceBuilder IPriceBuilder.WithIsActive(bool isActive)
            {
                Instance.IsActive = isActive;
                return this;
            }

            IPriceBuilder IPriceBuilder.WithValue(decimal value)
            {
                Instance.Value = value;
                return this;
            }

            protected override void Specifications()
            {
                AddSpecification<PriceShouldHaveValueSpecification>();
            }
        }
        public interface IPriceBuilder : IBuilder<Price>
        {
            IPriceBuilder WithCreateDate(DateTimeOffset createDate);
            IPriceBuilder WithLastChange(DateTimeOffset lastChange);
            IPriceBuilder WithProductId(Guid productId);
            IPriceBuilder WithStartDate(DateTimeOffset startDate);
            IPriceBuilder WithEndDate(DateTimeOffset endDate);
            IPriceBuilder WithIsActive(bool isActive);
            IPriceBuilder WithValue(decimal value);
        }
    }
}