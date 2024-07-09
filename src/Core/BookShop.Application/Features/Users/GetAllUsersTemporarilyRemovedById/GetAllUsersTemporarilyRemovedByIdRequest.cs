using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Users.GetAllUsersTemporarilyRemovedById;

/// <summary>
///     GetAllUsersTemporarilyRemovedById Request
/// </summary>
public class GetAllUsersTemporarilyRemovedByIdRequest
    : IFeatureRequest<GetAllUsersTemporarilyRemovedByIdResponse>
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
