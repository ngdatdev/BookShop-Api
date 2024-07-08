using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.CartItems.UpdateCartItemById;

/// <summary>
///    UpdateCartItemById Request Validator
/// </summary>
public sealed class UpdateCartItemByIdRequestValidator
    : FeatureRequestValidator<UpdateCartItemByIdRequest, UpdateCartItemByIdResponse>
{
    public UpdateCartItemByIdRequestValidator()
    {
        RuleFor(request => request.CartItemId).NotEmpty();

        RuleFor(request => request.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(CartItem.MetaData.Quantity.MinValue);
    }
}
