using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Orders.CreateOrder;

/// <summary>
///    CreateOrder Request Validator
/// </summary>
public sealed class CreateOrderRequestValidator
    : FeatureRequestValidator<CreateOrderRequest, CreateOrderResponse>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(request => request.CartItems).NotEmpty();

        RuleFor(request => request.ShippingAddress)
            .NotEmpty()
            .Matches(@"^.+<token\/>.+<token\/>.+$");

        RuleFor(expression: request => request.PaymentMethod)
            .NotEmpty()
            .Must(method => method == "Cash" || method == "BankTransfer");
    }
}
