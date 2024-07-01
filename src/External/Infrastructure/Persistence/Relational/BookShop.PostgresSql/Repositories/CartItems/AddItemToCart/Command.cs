using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.PostgresSql.Repositories.CartItems.AddItemToCart;

/// <summary>
///    Implement of command IAddItemToCartRepository.
/// </summary>
internal partial class AddItemToCartRepository
{
    public Task<bool> CreateCartItemCommandAsync(
        CartItem cartItems,
        CancellationToken cancellationToken
    )
    {
        throw new System.NotImplementedException();
    }
}
