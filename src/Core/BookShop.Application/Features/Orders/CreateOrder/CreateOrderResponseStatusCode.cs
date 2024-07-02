namespace BookShop.Application.Features.CartItems.CreateOrder;

/// <summary>
///     CreateOrder Response Status Code
/// </summary>
public enum CreateOrderResponseStatusCode
{
    PRODUCTS_IS_NOT_FOUND,
    PRODUCTS_IS_TEMPORARILY_REMOVED,
    CART_ID_IS_NOT_FOUND,
    QUANTITY_IS_NOT_ENOUGH,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ADDRESS_IS_NOT_CORRECT_FORMAT
}
