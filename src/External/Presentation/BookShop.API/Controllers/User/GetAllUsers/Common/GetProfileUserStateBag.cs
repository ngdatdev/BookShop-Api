using BookShop.Application.Features.Users.GetAllUsers;

namespace BookShop.API.Controllers.User.GetAllUsers.Common;

/// <summary>
///     Represents the GetAllUsers state bag.
/// </summary>
internal sealed class GetAllUsersStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
