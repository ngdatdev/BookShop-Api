namespace BookShop.API.Controllers.User.GetProfileUserEndpoint.Common;

/// <summary>
///     Represents the login state bag.
/// </summary>
internal sealed class GetProfileUserStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
