using System;

namespace BookShop.Data.Shared.Entities.Base;

/// <summary>
///     Use in case the entity requires the information
///     about the person updating the entity and the time
///     is it updated.
/// </summary>
public interface IUpdatedEntity
{
    /// <summary>
    ///     When is entity updated.
    /// </summary>
    DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     Id of the entity updater.
    /// </summary>
    Guid UpdatedBy { get; set; }
}
