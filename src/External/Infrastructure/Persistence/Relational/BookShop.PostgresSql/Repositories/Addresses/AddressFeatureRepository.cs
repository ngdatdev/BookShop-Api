using BookShop.Data.Features.Repositories.Address;
using BookShop.Data.Features.Repositories.Addresses.GetAddressesByWard;
using BookShop.Data.Features.Repositories.Addresses.GetAllDistrictsByProvinceName;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Addresses.GetAddressesByWard;
using BookShop.PostgresSql.Repositories.Addresses.GetAllDistrictsByProvinceName;

namespace BookShop.PostgresSql.Repositories.Carts;

/// <summary>
///    Implement of CartFeatureRepository interface.
/// </summary>
internal class AddressFeatureRepository : IAddressFeatureRepository
{
    private readonly BookShopContext _context;
    private IGetAddressesByWardRepository _getCartByIdRepository;
    private IGetAllDistrictsByProvinceNameRepository _getAllDistrictByIdRepository;

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

    public IGetAllDistrictsByProvinceNameRepository GetAllDistrictsByProvinceNameRepository
    {
        get
        {
            return _getAllDistrictByIdRepository ??= new GetAllDistrictsByProvinceNameRepository(
                context: _context
            );
        }
    }
}
