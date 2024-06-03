using System;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "Reviews" table.
/// </summary>
public class Review : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties.
    public string Comment { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign key.
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    // Navigation properties.
    public UserDetail UserDetail { get; set; }

    public Product Product { get; set; }

    public static class MetaData
    {
        public static class Comment
        {
            public const int MinLength = 2;
            public const int MaxLength = 200;
        }
    }
}
