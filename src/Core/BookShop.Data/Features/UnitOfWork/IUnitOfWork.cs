using BookShop.Data.Features.Repositories.Auth;
using BookShop.Data.Features.Repositories.CartItems;
using BookShop.Data.Features.Repositories.Carts;
using BookShop.Data.Features.Repositories.Product;
using BookShop.Data.Features.Repositories.User;

namespace BookShop.Data.Features.UnitOfWork;

/// <summary>
///     Represent the base unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///    Auth repository manager.
    /// </summary>
    public IAuthFeatureRepository AuthFeature { get; }

    /// <summary>
    ///    User repository manager.
    /// </summary>
    public IUserFeatureRepository UserFeature { get; }

    /// <summary>
    ///    Product repository manager.
    /// </summary>
    public IProductFeatureRepository ProductFeature { get; }

    /// <summary>
    ///    Cart repository manager.
    /// </summary>
    public ICartFeatureRepository CartFeature { get; }

    /// <summary>
    ///    Cart Item repository manager.
    /// </summary>
    public ICartItemFeatureRepository CartItemFeature { get; }
}
