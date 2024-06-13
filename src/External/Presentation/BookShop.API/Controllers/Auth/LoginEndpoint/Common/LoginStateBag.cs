namespace BookShop.API.Controllers.Auth.LoginEndpoint.Common;

/// <summary>
///     Represents the login state bag.
/// </summary>
internal sealed class LoginStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
