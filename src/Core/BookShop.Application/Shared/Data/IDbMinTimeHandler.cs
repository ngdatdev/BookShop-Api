using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Application.Shared.Data;

/// <summary>
///     Represent the handler for database min time.
/// </summary>
public interface IDbMinTimeHandler
{
    /// <summary>
    ///     Get the value of database min time.
    /// </summary>
    DateTime Get();
}
