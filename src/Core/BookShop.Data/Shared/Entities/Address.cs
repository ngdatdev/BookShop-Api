using System;
using System.Collections.Generic;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "Address" table.
/// </summary>
public class Address : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties.
    public string Ward { get; set; }

    public string District { get; set; }

    public string Province { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation Collections.
    public IEnumerable<UserDetail> UserDetails { get; set; }

    public IEnumerable<Order> Orders { get; set; }

    public static class MetaData
    {
        public static class Ward
        {
            public const int MinLength = 10;

            public const int MaxLength = 50;
        }

        public static class District
        {
            public const int MinLength = 10;

            public const int MaxLength = 50;
        }

        public static class Province
        {
            public const int MinLength = 10;

            public const int MaxLength = 50;
        }
    }
}
