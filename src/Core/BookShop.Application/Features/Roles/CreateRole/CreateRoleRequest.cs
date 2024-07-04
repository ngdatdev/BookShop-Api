using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Roles.CreateRole;

/// <summary>
///     CreateRole Request
/// </summary>
public class CreateRoleRequest : IFeatureRequest<CreateRoleResponse>
{
    public string RoleName { get; init; }
}
