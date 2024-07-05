using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Roles.UpdateRoleById;

/// <summary>
///     UpdateRoleById Request
/// </summary>
public class UpdateRoleByIdRequest : IFeatureRequest<UpdateRoleByIdResponse>
{
    public Guid RoleId { get; init; }
    public string RoleName { get; init; }
}
