using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.Auth.ForgotPassword;

/// <summary>
///     ForgotPassword Request Validator
/// </summary>
public sealed class ForgotPasswordRequestValidator
    : FeatureRequestValidator<ForgotPasswordRequest, ForgotPasswordResponse>
{
    public ForgotPasswordRequestValidator()
    {
        RuleFor(expression: request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(maximumLength: User.MetaData.Email.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.Email.MinLength);
    }
}
