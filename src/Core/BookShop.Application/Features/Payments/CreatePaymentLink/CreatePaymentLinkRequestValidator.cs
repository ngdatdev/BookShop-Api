using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Payments.CreatePaymentLink;

/// <summary>
///    CreatePaymentLink Request Validator
/// </summary>
public sealed class CreatePaymentLinkRequestValidator
    : FeatureRequestValidator<CreatePaymentLinkRequest, CreatePaymentLinkResponse>
{
    public CreatePaymentLinkRequestValidator()
    {
        RuleFor(expression: request => request.OrderCode).NotEmpty().GreaterThanOrEqualTo(0);

        RuleFor(request => request.Amount).NotEmpty().GreaterThanOrEqualTo(0);

        RuleFor(expression: request => request.Items).NotEmpty();

        RuleFor(expression: request => request.ReturnUrl)
            .MinimumLength(minimumLength: 0)
            .NotEmpty();

        RuleFor(expression: request => request.CancelUrl)
            .MinimumLength(minimumLength: 0)
            .NotEmpty();
    }
}
