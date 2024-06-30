using BookShop.Application.Features.Users.GetProfileUser;

namespace BookShop.API.Controllers.User.GetProfileUser.Common;

/// <summary>
///     Represents the GetProfileUser state bag.
/// </summary>
internal sealed class GetProfileUserStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
