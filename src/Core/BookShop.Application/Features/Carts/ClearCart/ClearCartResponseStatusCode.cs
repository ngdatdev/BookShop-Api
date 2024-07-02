namespace BookShop.Application.Features.Carts.ClearCart;

/// <summary>
///     ClearCart Response Status Code
/// </summary>
public enum ClearCartResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    CART_IS_NOT_FOUND,
}
