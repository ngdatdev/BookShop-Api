using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BookShop.API.Controllers.Product.GetProductByIdEndpoint.Common;

/// <summary>
///     Represents the get GetProductById state bag.
/// </summary>
internal sealed class GetProductByIdStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
