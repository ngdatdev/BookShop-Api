using System;
using System.Collections.Generic;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "Orders" table.
/// </summary>
public class Order : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties.
    public DateTime OrderDate { get; set; }

    public decimal TotalCost { get; set; }

    public DateTime ExpectedDate { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign key.
    public Guid UserId { get; set; }

    public Guid AddressId { get; set; }

    // Navigation properties.
    public UserDetail UserDetail { get; set; }

    public Address Address { get; set; }

    // Navigation collections.
    public IEnumerable<OrderDetail> OrderDetails { get; set; }

    public static class MetaData
    {
        public static class TotalCost
        {
            public const decimal MinValue = 0;
        }
    }
}
