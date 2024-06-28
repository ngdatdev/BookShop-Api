using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Users.RestoreUserById;

/// <summary>
///    RestoreUserById Request Validator
/// </summary>
public sealed class RestoreUserByIdRequestValidator
    : FeatureRequestValidator<RestoreUserByIdRequest, RestoreUserByIdResponse>
{
    public RestoreUserByIdRequestValidator()
    {
        RuleFor(request => request.UserId).NotEmpty();
    }
}
