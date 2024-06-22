using BookShop.Data.Features.Repositories.Product;
using BookShop.Data.Features.Repositories.Product.CreateProduct;
using BookShop.Data.Features.Repositories.Product.GetAllProducts;
using BookShop.Data.Features.Repositories.Product.GetProductsByCategoryId;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Product.CreateProduct;
using BookShop.PostgresSql.Repositories.Product.GetAllProducts;
using BookShop.PostgresSql.Repositories.Product.GetProductsByCategoryId;

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
}
