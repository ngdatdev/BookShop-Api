using BookShop.Data.Features.Repositories.CartItems.AddItemToCart;
using BookShop.Data.Features.Repositories.CartItems.RemoveCartItemById;
using BookShop.Data.Features.Repositories.CartItems.UpdateCartItemById;

namespace BookShop.Data.Features.Repositories.CartItems;

/// <summary>
///     Interface for cart item repository manager.
/// </summary>
public interface ICartItemFeatureRepository
{
    /// <summary>
    ///     Gets add item to cart feature repository.
    /// </summary>
    public IAddItemToCartRepository AddItemToCartRepository { get; }

    /// <summary>
    ///     Gets update cart item by id feature repository.
    /// </summary>
    public IUpdateCartItemByIdRepository UpdateCartItemByIdRepository { get; }

    /// <summary>
    ///     Gets remove cart item by id feature repository.
    /// </summary>
    public IRemoveCartItemByIdRepository RemoveCartItemByIdRepository { get; }
}
