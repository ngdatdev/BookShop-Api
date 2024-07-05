using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Roles.RestoreRoleById;

/// <summary>
///     RestoreRoleById Request
/// </summary>
public class RestoreRoleByIdRequest : IFeatureRequest<RestoreRoleByIdResponse>
{
    public Guid RoleId { get; set; }
}
