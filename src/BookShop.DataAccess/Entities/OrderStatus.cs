using System;
using BookShop.DataAccess.Entities.Base;

namespace BookShop.DataAccess.Entities;

/// <summary>
///     Represent the "OrderStatus" enum.
/// </summary>
public class OrderStatus : IBaseEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties.
    public string FullName { get; set; }
}
