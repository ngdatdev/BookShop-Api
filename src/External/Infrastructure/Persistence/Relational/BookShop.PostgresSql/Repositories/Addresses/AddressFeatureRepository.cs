using BookShop.Data.Features.Repositories.Address;
using BookShop.Data.Features.Repositories.Addresses.GetAddressesByWard;
using BookShop.Data.Features.Repositories.Addresses.GetAllDistrictsByProvinceName;
using BookShop.Data.Features.Repositories.Addresses.GetAllTemporarilyAddresses;
using BookShop.Data.Features.Repositories.Addresses.GetAllWardsByDistrictName;
using BookShop.Data.Features.Repositories.Addresses.RemoveAddressPermanentlyRemovedById;
using BookShop.Data.Features.Repositories.Addresses.RemoveAddressTemporarilyRemovedById;
using BookShop.Data.Features.Repositories.Addresses.RestoreAddressById;
using BookShop.Data.Features.Repositories.Addresses.UpdateAddressById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Addresses.GetAddressesByWard;
using BookShop.PostgresSql.Repositories.Addresses.GetAllDistrictsByProvinceName;
using BookShop.PostgresSql.Repositories.Addresses.GetAllTemporarilyAddresses;
using BookShop.PostgresSql.Repositories.Addresses.GetAllWardsByDistrictName;
using BookShop.PostgresSql.Repositories.Addresses.RemoveAddressPermanentlyRemovedById;
using BookShop.PostgresSql.Repositories.Addresses.RemoveAddressTemporarilyRemovedById;
using BookShop.PostgresSql.Repositories.Addresses.RestoreAddressById;
using BookShop.PostgresSql.Repositories.Addresses.UpdateAddressById;

namespace BookShop.PostgresSql.Repositories.Carts;

/// <summary>
///    Implement of CartFeatureRepository interface.
/// </summary>
internal class AddressFeatureRepository : IAddressFeatureRepository
{
    private readonly BookShopContext _context;
    private IGetAddressesByWardRepository _getCartByIdRepository;
    private IGetAllDistrictsByProvinceNameRepository _getAllDistrictByIdRepository;
    private IGetAllWardsByDistrictNameRepository _getAllWardsByDistrictNameRepository;
    private IUpdateAddressByIdRepository _updateAddressByIdRepository;
    private IRemoveAddressTemporarilyRemovedByIdRepository _removeAddressTemporarilyRemovedByIdRepository;
    private IRemoveAddressPermanentlyRemovedByIdRepository _removeAddressPermanentlyRemovedByIdRepository;
    private IRestoreAddressByIdRepository _restoreAddressByIdRepository;
    private IGetAllTemporarilyAddressesRepository _getAllTemporarilyAddressesRepository;

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

    public IGetAllWardsByDistrictNameRepository GetAllWardsByDistrictNameRepository
    {
        get
        {
            return _getAllWardsByDistrictNameRepository ??= new GetAllWardsByDistrictNameRepository(
                context: _context
            );
        }
    }

    public IUpdateAddressByIdRepository UpdateAddressByIdRepository
    {
        get
        {
            return _updateAddressByIdRepository ??= new UpdateAddressByIdRepository(
                context: _context
            );
        }
    }

    public IRemoveAddressTemporarilyRemovedByIdRepository RemoveAddressTemporarilyRemovedByIdRepository
    {
        get
        {
            return _removeAddressTemporarilyRemovedByIdRepository ??=
                new RemoveAddressTemporarilyRemovedByIdRepository(context: _context);
        }
    }

    public IRemoveAddressPermanentlyRemovedByIdRepository RemoveAddressPermanentlyRemovedByIdRepository
    {
        get
        {
            return _removeAddressPermanentlyRemovedByIdRepository ??=
                new RemoveAddressPermanentlyRemovedByIdRepository(context: _context);
        }
    }

    public IRestoreAddressByIdRepository RestoreAddressByIdRepository
    {
        get
        {
            return _restoreAddressByIdRepository ??= new RestoreAddressByIdRepository(
                context: _context
            );
        }
    }

    public IGetAllTemporarilyAddressesRepository GetAllTemporarilyAddressesRepository
    {
        get
        {
            return _getAllTemporarilyAddressesRepository ??=
                new GetAllTemporarilyAddressesRepository(context: _context);
        }
    }
}
