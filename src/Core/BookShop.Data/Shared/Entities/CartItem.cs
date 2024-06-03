using System;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "CartItem" table.
/// </summary>
public class CartItem : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties.
    public int Quantity { get; set; }

    public decimal TotalCost { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign key.
    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    // Navigation properties.
    public Cart Cart { get; set; }

    public Product Product { get; set; }

    public static class MetaData
    {
        public static class Quantity
        {
            public const int MinValue = 0;
        }

        public static class TotalCost
        {
            public const decimal MinValue = 0;
        }
    }
}
