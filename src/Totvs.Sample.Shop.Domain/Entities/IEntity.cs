using System;

namespace Totvs.Sample.Shop.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTimeOffset CreateDate { get; set; }
        DateTimeOffset LastChange { get; set; }
    }
}
