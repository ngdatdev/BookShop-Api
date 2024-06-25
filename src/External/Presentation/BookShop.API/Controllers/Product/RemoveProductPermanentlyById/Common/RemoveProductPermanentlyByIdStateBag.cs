using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BookShop.API.Controllers.Product.RemoveProductPermanentlyById.Common;

/// <summary>
///     Represents the get RemoveProductPermanentlyById state bag.
/// </summary>
internal sealed class RemoveProductPermanentlyByIdStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
