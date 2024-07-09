namespace BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.Common;

/// <summary>
///     Represents the GetAllUsersTemporarilyRemovedById state bag.
/// </summary>
internal sealed class GetAllUsersTemporarilyRemovedByIdStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
