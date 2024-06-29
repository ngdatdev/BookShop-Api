using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.Auth.RegisterAsUser;

/// <summary>
///     RegisterAsUser Request Validator
/// </summary>
public sealed class RegisterAsUserRequestValidator
    : FeatureRequestValidator<RegisterAsUserRequest, RegisterAsUserResponse>
{
    public RegisterAsUserRequestValidator()
    {
        RuleFor(expression: request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(minimumLength: User.MetaData.Email.MinLength)
            .MaximumLength(maximumLength: User.MetaData.Email.MaxLength);

        RuleFor(expression: request => request.Username)
            .NotEmpty()
            .MinimumLength(minimumLength: User.MetaData.UserName.MinLength)
            .MaximumLength(maximumLength: User.MetaData.UserName.MaxLength);

        RuleFor(expression: request => request.Password)
            .NotEmpty()
            .MinimumLength(minimumLength: User.MetaData.Password.MinLength)
            .MaximumLength(maximumLength: User.MetaData.Password.MaxLength);
    }
}
