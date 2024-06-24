using BookShop.Data.Features.Repositories.Product.CreateProduct;
using BookShop.Data.Features.Repositories.Product.GetAllProducts;
using BookShop.Data.Features.Repositories.Product.GetAllTemporarilyRemovedProducts;
using BookShop.Data.Features.Repositories.Product.GetProductsByAuthorName;
using BookShop.Data.Features.Repositories.Product.GetProductsByCategoryId;
using BookShop.Data.Features.Repositories.Product.RemoveProductPermanentlyById;
using BookShop.Data.Features.Repositories.Product.RemoveProductTemporarilyById;
using BookShop.Data.Features.Repositories.Product.UpdateProductById;

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

    /// <summary>
    ///     Gets get products by id of category feature repository.
    /// </summary>
    public IGetProductsByCategoryIdRepository GetProductsByCategoryIdRepository { get; }

    /// <summary>
    ///     Gets update products by id feature repository.
    /// </summary>
    public IUpdateProductByIdRepository UpdateProductByIdRepository { get; }

    /// <summary>
    ///     Gets get products by author name feature repository.
    /// </summary>
    public IGetProductsByAuthorNameRepository GetProductsByAuthorNameRepository { get; }

    /// <summary>
    ///     Gets remove product temporarily by id feature repository.
    /// </summary>
    public IRemoveProductTemporarilyByIdRepository RemoveProductTemporarilyByIdRepository { get; }

    /// <summary>
    ///     Gets remove product permanently by id feature repository.
    /// </summary>
    public IRemoveProductPermanentlyByIdRepository RemoveProductPermanentlyByIdRepository { get; }

    /// <summary>
    ///     Gets get all temporarily removed product feature repository.
    /// </summary>
    public IGetAllTemporarilyRemovedProductsRepository GetAllTemporarilyRemovedProductsRepository { get; }
}
