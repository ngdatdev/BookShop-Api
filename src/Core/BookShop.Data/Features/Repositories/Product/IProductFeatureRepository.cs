using BookShop.Data.Features.Repositories.Product.CreateProduct;
using BookShop.Data.Features.Repositories.Product.GetAllProducts;

namespace BookShop.Data.Features.Repositories.Product;

/// <summary>
///     Interface for product repository manager.
/// </summary>
public interface IProductFeatureRepository
{
    /// <summary>
    ///     Gets get all products feature repository.
    /// </summary>
    public IGetAllProductsRepository GetAllProductsRepository { get; }

    /// <summary>
    ///     Gets create products feature repository.
    /// </summary>
    public ICreateProductRepository CreateProductRepository { get; }
}
