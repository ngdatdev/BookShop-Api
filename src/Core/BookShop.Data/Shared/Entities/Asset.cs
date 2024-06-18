using System;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "Asset" table.
/// </summary>
public class Asset : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties.
    public string ImageUrl { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign Key.
    public Guid ProductId { get; set; }

    // Navigation properties.
    public Product Product { get; set; }

    public static class MetaData
    {
        public static class ImageUrl
        {
            public const int MinLength = 2;

            public const int MaxLength = 400;
        }
    }
}
