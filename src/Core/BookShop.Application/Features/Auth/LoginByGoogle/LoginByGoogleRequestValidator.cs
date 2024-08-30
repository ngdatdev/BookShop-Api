using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.Auth.LoginByGoogle;

/// <summary>
///     LoginByGoogle Request Validator
/// </summary>
public sealed class LoginByGoogleRequestValidator
    : FeatureRequestValidator<LoginByGoogleRequest, LoginByGoogleResponse>
{
    public LoginByGoogleRequestValidator()
    {
        RuleFor(expression: request => request.IdToken).NotEmpty();
    }
}
