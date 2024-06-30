using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.Auth.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     ResendUserRegistrationConfirmedEmail Request Validator
/// </summary>
public sealed class ResendUserRegistrationConfirmedEmailRequestValidator
    : FeatureRequestValidator<
        ResendUserRegistrationConfirmedEmailRequest,
        ResendUserRegistrationConfirmedEmailResponse
    >
{
    public ResendUserRegistrationConfirmedEmailRequestValidator()
    {
        RuleFor(expression: request => request.Username)
            .NotEmpty()
            .MinimumLength(minimumLength: User.MetaData.UserName.MinLength)
            .MaximumLength(maximumLength: User.MetaData.UserName.MaxLength);
    }
}
