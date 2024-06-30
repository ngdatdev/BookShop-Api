namespace BookShop.API.Controllers.User.UpdateUserById.Common;

/// <summary>
///     Represents the UpdateUserById state bag.
/// </summary>
internal sealed class UpdateUserByIdStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
