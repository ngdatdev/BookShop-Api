using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Payments.UpdatePaymentCOD;

/// <summary>
///    UpdatePaymentCOD Request Validator
/// </summary>
public sealed class UpdatePaymentCODRequestValidator
    : FeatureRequestValidator<UpdatePaymentCODRequest, UpdatePaymentCODResponse>
{
    public UpdatePaymentCODRequestValidator()
    {
        RuleFor(expression: request => request.OrderId).NotEmpty();

        RuleFor(expression: request => request.Status)
            .Must(method => method == "Paid" || method == "Pending")
            .NotEmpty();

        RuleFor(expression: request => request.PaymentDate).NotEmpty();
    }
}
