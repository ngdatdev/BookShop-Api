using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data.Entities.Base;

/// <summary>
///     Use in case the entity requires the information
///     about the person removing the entity temporarily
///     and the time is it temporarily removed.
/// </summary>
public interface ITemporarilyRemovedEntity
{
    /// <summary>
    ///     When is entity temporarily removed.
    /// </summary>
    DateTime RemovedAt { get; set; }

    /// <summary>
    ///     Id of the entity remover.
    /// </summary>
    Guid RemovedBy { get; set; }
}
