using System;
using System.Collections.Generic;
using BookShop.DataAccess.Entities.Base;

namespace BookShop.DataAccess.Entities;

/// <summary>
///     Represent the "Products" table.
/// </summary>
public class Product : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary Key.
    public Guid Id { get; set; }

    // Normal columns.
    public string FullName { get; set; }

    public string Description { get; set; }

    public int QuantityCurrent { get; set; }

    public int QuantitySold { get; set; }

    public string ImageUrl { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign keys.
    public Guid CategoryId { get; set; }

    // Navigation properties.
    public Category Category { get; set; }

    // Navigation collections.
    public IEnumerable<Review> Reviews { get; set; }

    public IEnumerable<OrderDetail> OrderDetails { get; set; }

    public IEnumerable<CartItem> CartItems { get; set; }

    public static class MetaData
    {
        public static class FullName
        {
            public const int MinLength = 2;
            public const int MaxLength = 100;
        }

        public static class Description
        {
            public const int MinLength = 2;
        }

        public static class QuantityCurrent
        {
            public const int MinValue = 0;
        }

        public static class QuantitySold
        {
            public const int MinValue = 0;
        }

        public static class ImageUrl
        {
            public const int MinLength = 2;
        }
    }
}
