using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Users.UpdateUserById;

/// <summary>
///     UpdateUserById Request
/// </summary>
public class UpdateUserByIdRequest : IFeatureRequest<UpdateUserByIdResponse>
{
    public string Email { get; init; }

    public string Username { get; init; }

    public string NumberPhone { get; init; }

    public string OldAvatarUrl { get; init; }

    public IFormFile NewAvatarUrl { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Gender { get; init; }

    public DateTime DateOfBirth { get; set; }

    public string Address { get; init; }
}
