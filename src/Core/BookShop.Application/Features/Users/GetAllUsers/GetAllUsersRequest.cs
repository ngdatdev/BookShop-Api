using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Users.GetAllUsers;

/// <summary>
///     GetAllUsers Request
/// </summary>
public class GetAllUsersRequest : IFeatureRequest<GetAllUsersResponse>
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
