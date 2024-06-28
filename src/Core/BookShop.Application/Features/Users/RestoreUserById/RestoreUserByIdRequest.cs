using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Users.RestoreUserById;

/// <summary>
///     RestoreUserById Request
/// </summary>
public class RestoreUserByIdRequest : IFeatureRequest<RestoreUserByIdResponse>
{
    [FromRoute(Name = "user-id")]
    public Guid UserId { get; init; }
}
