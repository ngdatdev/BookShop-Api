using BookShop.Data.Features.Repositories.Address;
using BookShop.Data.Features.Repositories.Addresses.GetAddressesByWard;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Addresses.GetAddressesByWard;

namespace BookShop.PostgresSql.Repositories.Carts;

/// <summary>
///    Implement of CartFeatureRepository interface.
/// </summary>
internal class AddressFeatureRepository : IAddressFeatureRepository
{
    private readonly BookShopContext _context;
    private IGetAddressesByWardRepository _getCartByIdRepository;

    internal AddressFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IGetAddressesByWardRepository GetAddressesByWardRepository
    {
        get
        {
            return _getCartByIdRepository ??= new GetAddressesByWardRepository(context: _context);
        }
    }
}
