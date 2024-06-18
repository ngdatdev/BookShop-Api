using System;
using System.Collections.Generic;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

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

    public decimal Price { get; set; }

    public int Discount { get; set; }

    public string Size { get; set; }

    public int NumberOfPage { get; set; }

    public int QuantityCurrent { get; set; }

    public int QuantitySold { get; set; }

    public string ImageUrl { get; set; }

    public string Author { get; set; }

    public string Publisher { get; set; }

    public string Languages { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<Review> Reviews { get; set; }

    public IEnumerable<OrderDetail> OrderDetails { get; set; }

    public IEnumerable<CartItem> CartItems { get; set; }

    public IEnumerable<Asset> Assets { get; set; }

    public IEnumerable<ProductCategory> ProductCategories { get; set; }

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

        public static class Size
        {
            public const int MinValue = 0;
        }

        public static class NumberOfPage
        {
            public const int MinValue = 0;
        }

        public static class Author
        {
            public const int MinLength = 2;
            public const int MaxLength = 100;
        }

        public static class Publisher
        {
            public const int MinLength = 2;
            public const int MaxLength = 200;
        }

        public static class Languages
        {
            public const int MinLength = 4;
            public const int MaxLength = 30;
        }

        public static class Discount
        {
            public const int MinValue = 0;
            public const int MaxValue = 100;
        }

        public static class Price
        {
            public const int MinValue = 0;
        }
    }
}
