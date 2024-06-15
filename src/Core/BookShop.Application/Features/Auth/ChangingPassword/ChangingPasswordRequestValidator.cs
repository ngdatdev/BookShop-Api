using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.Auth.ChangingPassword;

/// <summary>
///     ChangingPassword Request Validator
/// </summary>
public sealed class ChangingPasswordRequestValidator
    : FeatureRequestValidator<ChangingPasswordRequest, ChangingPasswordResponse>
{
    public ChangingPasswordRequestValidator()
    {
        RuleFor(expression: request => request.NewPassword)
            .NotEmpty()
            .MaximumLength(maximumLength: User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.Password.MinLength);

        RuleFor(expression: request => request.ResetPasswordToken).NotEmpty();
    }
}
