using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Roles.CreateRole;

/// <summary>
///     CreateRole Response
/// </summary>
public class CreateRoleResponse : IFeatureResponse
{
    public CreateRoleResponseStatusCode StatusCode { get; init; }
}
