namespace BookShop.Application.Features.CartItems.UpdateCartItemById;

/// <summary>
///     UpdateCartItemById Response Status Code
/// </summary>
public enum UpdateCartItemByIdResponseStatusCode
{
    CART_ITEM_IS_NOT_FOUND,
    QUANTITY_IS_NOT_ENOUGH,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
