using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Roles.RemoveRoleTemporarilyById;

/// <summary>
///     RemoveRoleTemporarilyById Response
/// </summary>
public class RemoveRoleTemporarilyByIdResponse : IFeatureResponse
{
    public RemoveRoleTemporarilyByIdResponseStatusCode StatusCode { get; init; }
}
