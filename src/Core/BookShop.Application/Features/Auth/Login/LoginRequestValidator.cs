using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.Auth.Login;

/// <summary>
///     Login Request Validator
/// </summary>
public sealed class LoginRequestValidator : FeatureRequestValidator<LoginRequest, LoginResponse>
{
    public LoginRequestValidator()
    {
        RuleFor(expression: request => request.Username)
            .NotEmpty()
            .MaximumLength(maximumLength: User.MetaData.UserName.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.UserName.MinLength);

        RuleFor(expression: request => request.Password)
            .NotEmpty()
            .Matches(expression: @"^(?=.*\d)(?=.*[A-Z]).+$")
            .MaximumLength(maximumLength: User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.Password.MinLength);
    }
}
