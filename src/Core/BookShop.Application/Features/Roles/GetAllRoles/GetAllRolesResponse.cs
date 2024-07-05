using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Roles.GetAllRoles;

/// <summary>
///     GetAllRoles Response
/// </summary>
public class GetAllRolesResponse : IFeatureResponse
{
    public GetAllRolesResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Role> Roles { get; init; }

        public sealed class Role
        {
            public Guid RoleId { get; init; }
            public string RoleName { get; init; }
        }
    }
}
