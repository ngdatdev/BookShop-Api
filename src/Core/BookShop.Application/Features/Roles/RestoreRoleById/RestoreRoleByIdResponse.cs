using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Roles.RestoreRoleById;

/// <summary>
///     RestoreRoleById Response
/// </summary>
public class RestoreRoleByIdResponse : IFeatureResponse
{
    public RestoreRoleByIdResponseStatusCode StatusCode { get; init; }
}
