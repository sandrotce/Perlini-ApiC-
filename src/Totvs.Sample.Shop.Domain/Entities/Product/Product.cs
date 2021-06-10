using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace Totvs.Sample.Shop.Domain.Entities
{
    public partial class Product : IEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastChange { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<Price> Prices { get; set; }

        public static IProductBuilder Create (INotificationHandler handler) => new Builder (handler)
            .GenerateNewProduct ();

        public static IProductBuilder Create (INotificationHandler handler, Product instance) => new Builder (handler, instance);
    }
}