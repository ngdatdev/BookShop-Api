using BookShop.Data.Features.Repositories.Product;
using BookShop.Data.Features.Repositories.Product.CreateProduct;
using BookShop.Data.Features.Repositories.Product.GetAllProducts;
using BookShop.Data.Features.Repositories.Product.GetAllTemporarilyRemovedProducts;
using BookShop.Data.Features.Repositories.Product.GetProductsByAuthorName;
using BookShop.Data.Features.Repositories.Product.GetProductsByCategoryId;
using BookShop.Data.Features.Repositories.Product.RemoveProductPermanentlyById;
using BookShop.Data.Features.Repositories.Product.RemoveProductTemporarilyById;
using BookShop.Data.Features.Repositories.Product.UpdateProductById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Product.CreateProduct;
using BookShop.PostgresSql.Repositories.Product.GetAllProducts;
using BookShop.PostgresSql.Repositories.Product.GetAllTemporarilyRemovedProducts;
using BookShop.PostgresSql.Repositories.Product.GetProductsByAuthorName;
using BookShop.PostgresSql.Repositories.Product.GetProductsByCategoryId;
using BookShop.PostgresSql.Repositories.Product.RemoveProductPermanentlyById;
using BookShop.PostgresSql.Repositories.Product.RemoveProductTemporarilyById;
using BookShop.PostgresSql.Repositories.Product.UpdateProductById;

namespace BookShop.PostgresSql.Repositories.Product;

/// <summary>
///    Implement of ProductFeatureRepository interface.
/// </summary>
internal class ProductFeatureRepository : IProductFeatureRepository
{
    private readonly BookShopContext _context;

    private IGetAllProductsRepository _getAllProductsRepository;
    private ICreateProductRepository _createProductRepository;
    private IGetProductsByCategoryIdRepository _getProductsByCategoryIdRepository;
    private IUpdateProductByIdRepository _updateProductByIdRepository;
    private IGetProductsByAuthorNameRepository _getProductsByAuthorNameRepository;
    private IRemoveProductTemporarilyByIdRepository _removeProductTemporarilyByIdRepository;
    private IRemoveProductPermanentlyByIdRepository _removeProductPermanentlyByIdRepository;
    private IGetAllTemporarilyRemovedProductsRepository _getAllTemporarilyRemovedProductsRepository;

    internal ProductFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IGetAllProductsRepository GetAllProductsRepository
    {
        get
        {
            return _getAllProductsRepository ??= new GetAllProductsRepository(context: _context);
        }
    }

    public ICreateProductRepository CreateProductRepository
    {
        get { return _createProductRepository ??= new CreateProductRepository(context: _context); }
    }

    public IGetProductsByCategoryIdRepository GetProductsByCategoryIdRepository
    {
        get
        {
            return _getProductsByCategoryIdRepository ??= new GetProductsByCategoryIdRepository(
                context: _context
            );
        }
    }

    public IUpdateProductByIdRepository UpdateProductByIdRepository
    {
        get
        {
            return _updateProductByIdRepository ??= new UpdateProductByIdRepository(
                context: _context
            );
        }
    }

    public IGetProductsByAuthorNameRepository GetProductsByAuthorNameRepository
    {
        get
        {
            return _getProductsByAuthorNameRepository ??= new GetProductsByAuthorNameRepository(
                context: _context
            );
        }
    }

    public IRemoveProductTemporarilyByIdRepository RemoveProductTemporarilyByIdRepository
    {
        get
        {
            return _removeProductTemporarilyByIdRepository ??=
                new RemoveProductTemporarilyByIdRepository(context: _context);
        }
    }

    public IRemoveProductPermanentlyByIdRepository RemoveProductPermanentlyByIdRepository
    {
        get
        {
            return _removeProductPermanentlyByIdRepository ??=
                new RemoveProductPermanentlyByIdRepository(context: _context);
        }
    }

    public IGetAllTemporarilyRemovedProductsRepository GetAllTemporarilyRemovedProductsRepository
    {
        get
        {
            return _getAllTemporarilyRemovedProductsRepository ??=
                new GetAllTemporarilyRemovedProductsRepository(context: _context);
        }
    }
}
