using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Users.RemoveUserTemporarilyById;

/// <summary>
///     RemoveUserTemporarilyById Request
/// </summary>
public class RemoveUserTemporarilyByIdRequest : IFeatureRequest<RemoveUserTemporarilyByIdResponse>
{
    [FromRoute(Name = "user-id")]
    public Guid UserId { get; init; }
}
