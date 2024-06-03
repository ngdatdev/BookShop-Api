using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Application.Shared.FileObjectStorage;

/// <summary>
///     Represent the handler for default user avatar as url.
/// </summary>
public interface IDefaultUserAvatarAsUrlHandler
{
    /// <summary>
    ///     Get the default user avatar as url.
    /// </summary>
    string Get();
}
