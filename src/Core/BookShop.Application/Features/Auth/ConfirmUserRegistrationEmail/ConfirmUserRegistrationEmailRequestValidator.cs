using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.Auth.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail Request Validator
/// </summary>
public sealed class ConfirmUserRegistrationEmailRequestValidator
    : FeatureRequestValidator<
        ConfirmUserRegistrationEmailRequest,
        ConfirmUserRegistrationEmailResponse
    >
{
    public ConfirmUserRegistrationEmailRequestValidator()
    {
        RuleFor(expression: request => request.UserRegistrationEmailConfirmedTokenAsBase64)
            .NotEmpty();
    }
}
