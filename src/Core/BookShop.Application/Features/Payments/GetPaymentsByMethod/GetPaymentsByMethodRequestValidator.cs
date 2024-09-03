using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Payments.GetPaymentsByMethod;

/// <summary>
///    GetPaymentsByMethod Request Validator
/// </summary>
public sealed class GetPaymentsByMethodRequestValidator
    : FeatureRequestValidator<GetPaymentsByMethodRequest, GetPaymentsByMethodResponse>
{
    public GetPaymentsByMethodRequestValidator()
    {
        RuleFor(expression: request => request.Method)
            .NotEmpty()
            .Must(method => method == "Cash" || method == "BankTransfer");
    }
}
