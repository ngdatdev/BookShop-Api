using System;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

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
