using BookShop.Data.Features.Repositories.CartItems.AddItemToCart;

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
}
