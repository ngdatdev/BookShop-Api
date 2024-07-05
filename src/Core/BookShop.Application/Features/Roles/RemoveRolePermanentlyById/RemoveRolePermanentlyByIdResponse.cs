using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Roles.RemoveRolePermanentlyById;

/// <summary>
///     RemoveRolePermanentlyById Response
/// </summary>
public class RemoveRolePermanentlyByIdResponse : IFeatureResponse
{
    public RemoveRolePermanentlyByIdResponseStatusCode StatusCode { get; init; }
}
