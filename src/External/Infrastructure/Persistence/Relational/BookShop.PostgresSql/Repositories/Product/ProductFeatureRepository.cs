using BookShop.Data.Features.Repositories.Product;
using BookShop.Data.Features.Repositories.Product.GetAllProducts;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Product.GetAllProducts;
using Microsoft.AspNetCore.Identity;

namespace BookShop.PostgresSql.Repositories.Product;

/// <summary>
///    Implement of ProductFeatureRepository interface.
/// </summary>
internal class ProductFeatureRepository : IProductFeatureRepository
{
    private readonly BookShopContext _context;

    private IGetAllProductsRepository _getAllProductsRepository;

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
}
