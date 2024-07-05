using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Roles.RemoveRoleTemporarilyById;

/// <summary>
///     RemoveRoleTemporarilyById Request
/// </summary>
public class RemoveRoleTemporarilyByIdRequest : IFeatureRequest<RemoveRoleTemporarilyByIdResponse>
{
    public Guid RoleId { get; set; }
}
