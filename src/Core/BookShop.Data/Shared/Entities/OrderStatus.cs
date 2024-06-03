using System;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "OrderStatus" enum.
/// </summary>
public class OrderStatus : IBaseEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties.
    public string FullName { get; set; }

    public static class MetaData
    {
        public static class FullName
        {
            public const int MinLength = 2;
            public const int MaxLength = 100;
        }
    }
}
