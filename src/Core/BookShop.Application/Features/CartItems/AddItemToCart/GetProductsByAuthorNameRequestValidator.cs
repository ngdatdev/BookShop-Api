using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.CartItems.AddItemToCart;

/// <summary>
///    AddItemToCart Request Validator
/// </summary>
public sealed class AddItemToCartRequestValidator
    : FeatureRequestValidator<AddItemToCartRequest, AddItemToCartResponse>
{
    public AddItemToCartRequestValidator()
    {
        RuleFor(request => request.ProductId).NotEmpty();

        RuleFor(request => request.Quantity).NotEmpty().GreaterThanOrEqualTo(0);
    }
}
