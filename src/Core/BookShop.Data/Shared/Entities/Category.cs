using System;
using System.Collections.Generic;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "Category" enum.
/// </summary>
public class Category : IBaseEntity
{
    // Primary Key.
    public Guid Id { get; set; }

    // Normal properties
    public string FullName { get; set; }
    public string Description { get; set; }

    // Navigation collections.
    public IEnumerable<Product> Products { get; set; }

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
    }
}
