using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace Totvs.Sample.Shop.Domain.Entities
{
    public partial class Price : IEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastChange { get; set; }
        public Guid ProductId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Value { get; set; }
        public virtual Product Product { get; set; }

        public static IPriceBuilder Create (INotificationHandler handler) => new Builder (handler)
            .GenerateNewPrice ();

        public static IPriceBuilder Create (INotificationHandler handler, Price instance) => new Builder (handler, instance);
    }
}