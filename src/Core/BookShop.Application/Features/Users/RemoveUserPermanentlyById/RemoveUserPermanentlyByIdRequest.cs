using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Users.RemoveUserPermanentlyById;

/// <summary>
///     RemoveUserPermanentlyById Request
/// </summary>
public class RemoveUserPermanentlyByIdRequest : IFeatureRequest<RemoveUserPermanentlyByIdResponse>
{
    [FromRoute(Name = "user-id")]
    public Guid UserId { get; init; }
}
