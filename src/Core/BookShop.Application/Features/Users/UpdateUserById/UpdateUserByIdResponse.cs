using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Users.UpdateUserById;

/// <summary>
///     UpdateUserById Response
/// </summary>
public class UpdateUserByIdResponse : IFeatureResponse
{
    public UpdateUserByIdResponseStatusCode StatusCode { get; init; }
}
