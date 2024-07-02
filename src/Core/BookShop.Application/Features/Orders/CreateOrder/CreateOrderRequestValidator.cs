using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.CartItems.CreateOrder;

/// <summary>
///    CreateOrder Request Validator
/// </summary>
public sealed class CreateOrderRequestValidator
    : FeatureRequestValidator<CreateOrderRequest, CreateOrderResponse>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(request => request.CartItems).NotEmpty();

        RuleFor(x => x.ShippingAddress).NotEmpty().Matches(@"^.+<token\/>.+<token\/>.+$");
    }
}
