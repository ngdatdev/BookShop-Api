namespace BookShop.Application.Features.CartItems.RemoveCartItemById;

/// <summary>
///     RemoveCartItemById Response Status Code
/// </summary>
public enum RemoveCartItemByIdResponseStatusCode
{
    CART_ITEM_IS_NOT_FOUND,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
