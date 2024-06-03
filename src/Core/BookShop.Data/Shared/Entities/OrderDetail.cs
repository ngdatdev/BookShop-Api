using System;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "OrderDetails" table.
/// </summary>
public class OrderDetail : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties
    public int Quantity { get; set; }

    public decimal Cost { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign keys.
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    // Navigation properties.
    public Order Order { get; set; }

    public Product Product { get; set; }

    public static class MetaData
    {
        public static class Quantity
        {
            public const int MinValue = 0;
            public const int MaxLength = 0;
        }

        public static class Cost
        {
            public const decimal MinValue = 0;
        }
    }
}
