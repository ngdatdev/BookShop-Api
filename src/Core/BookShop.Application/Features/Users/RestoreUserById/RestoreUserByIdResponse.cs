using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Users.RestoreUserById;

/// <summary>
///     RestoreUserById Response
/// </summary>
public class RestoreUserByIdResponse : IFeatureResponse
{
    public RestoreUserByIdResponseStatusCode StatusCode { get; init; }
}
