using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Data.Entities.Base;

namespace BookShop.Data.Entities;

/// <summary>
///     Represent the "Cart" table.
/// </summary>
public class Cart : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties.
    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign key.
    public Guid UserId { get; set; }

    // Navigation properties.
    public UserDetail UserDetail { get; set; }

    // Navigation collections.
    public IEnumerable<CartItem> CartItems { get; set; }
}
