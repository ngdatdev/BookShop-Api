using System;
using BookShop.Data.Entities.Base;

namespace BookShop.Data.Entities;

/// <summary>
///     Represent the "ProductCategory" table.
/// </summary>
public class ProductCategory : IBaseEntity
{
    // Primary key.
    // Foreign key.
    public Guid ProductId { get; set; }

    public Guid CategoryId { get; set; }

    // Navigation properties.
    public Product Product { get; set; }

    public Category Category { get; set; }
}
