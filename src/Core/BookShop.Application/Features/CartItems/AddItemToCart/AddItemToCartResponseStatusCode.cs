namespace BookShop.Application.Features.CartItems.AddItemToCart;

/// <summary>
///     AddItemToCart Response Status Code
/// </summary>
public enum AddItemToCartResponseStatusCode
{
    PRODUCT_IS_NOT_FOUND,
    CART_ID_IS_NOT_FOUND,
    QUANTITY_IS_NOT_ENOUGH,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
