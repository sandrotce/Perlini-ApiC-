using System;
using System.Collections.Generic;
using System.Linq;
using Tnf.Builder;
using Tnf.Notifications;
using Totvs.Sample.Shop.Domain.Entities.Specifications;

namespace Totvs.Sample.Shop.Domain.Entities
{
    public partial class Product
    {
        public class Builder : Builder<Product>, IProductBuilder
        {
            public Builder(INotificationHandler notificationHandler) : base(notificationHandler) { }

            public Builder(INotificationHandler notificationHandler, Product instance) : base(notificationHandler, instance) { }

            public Builder GenerateNewProduct()
            {
                Instance.Id = Guid.NewGuid();

                return this;
            }

            IProductBuilder IProductBuilder.WithCreateDate(DateTimeOffset createDate)
            {
                Instance.CreateDate = createDate;
                return this;
            }

            IProductBuilder IProductBuilder.WithLastChange(DateTimeOffset lastChange)
            {
                Instance.LastChange = lastChange;
                return this;
            }

            IProductBuilder IProductBuilder.WithCode(string code)
            {
                Instance.Code = code;
                return this;
            }

            IProductBuilder IProductBuilder.WithName(string name)
            {
                Instance.Name = name;
                return this;
            }

            IProductBuilder IProductBuilder.WithIsActive(bool isActive)
            {
                Instance.IsActive = isActive;
                return this;
            }

            protected override void Specifications()
            {

                AddSpecification<ProductShouldHaveNameSpecification>();
            }
        }

        public interface IProductBuilder : IBuilder<Product>
        {
            IProductBuilder WithCreateDate(DateTimeOffset createDate);
            IProductBuilder WithLastChange(DateTimeOffset lastChange);
            IProductBuilder WithCode(string code);
            IProductBuilder WithName(string name);
            IProductBuilder WithIsActive(bool isActive);
        }
    }
}