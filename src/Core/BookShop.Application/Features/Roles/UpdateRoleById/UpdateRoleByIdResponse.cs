using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Roles.UpdateRoleById;

/// <summary>
///     UpdateRoleById Response
/// </summary>
public class UpdateRoleByIdResponse : IFeatureResponse
{
    public UpdateRoleByIdResponseStatusCode StatusCode { get; init; }
}
