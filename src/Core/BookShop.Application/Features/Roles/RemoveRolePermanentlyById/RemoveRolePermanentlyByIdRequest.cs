using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Roles.RemoveRolePermanentlyById;

/// <summary>
///     RemoveRolePermanentlyById Request
/// </summary>
public class RemoveRolePermanentlyByIdRequest : IFeatureRequest<RemoveRolePermanentlyByIdResponse>
{
    public Guid RoleId { get; set; }
}
