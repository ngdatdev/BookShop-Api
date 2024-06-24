using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BookShop.API.Controllers.Product.GetProductsByAuthorNameEndpoint.Common;

/// <summary>
///     Represents the get GetProductsByAuthorName state bag.
/// </summary>
internal sealed class GetProductsByAuthorNameStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;

    public static string GenerateCacheKey(
        Dictionary<string, string> parameters,
        HttpRequest request
    )
    {
        var sortedParameters = parameters.OrderBy(kv => kv.Key);

        var keyBuilder = new StringBuilder();
        foreach (var kv in sortedParameters)
        {
            keyBuilder.Append($"{kv.Key}={kv.Value}&");
        }

        var key = keyBuilder.ToString().TrimEnd('&');

        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            return $"{request.RouteValues["author-name"]}_{Convert.ToBase64String(hash)}";
        }
    }
}
