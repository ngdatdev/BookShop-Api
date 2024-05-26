using System;
using BookShop.DataAccess.Entities.Base;

namespace BookShop.DataAccess.Entities;

/// <summary>
///     Represent the "Products" table.
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
