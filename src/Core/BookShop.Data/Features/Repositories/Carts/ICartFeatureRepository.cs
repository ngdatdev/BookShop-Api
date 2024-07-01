using BookShop.Data.Features.Repositories.Carts.GetCartById;

namespace BookShop.Data.Features.Repositories.Carts;

/// <summary>
///     Interface for cart repository manager.
/// </summary>
public interface ICartFeatureRepository
{
    /// <summary>
    ///     Gets get cart by id feature repository.
    /// </summary>
    public IGetCartByIdRepository GetCartByIdRepository { get; }
}
