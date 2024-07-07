using BookShop.Data.Features.Repositories.Addresses.GetAddressesByWard;
using BookShop.Data.Features.Repositories.Addresses.GetAllDistrictsByProvinceName;
using BookShop.Data.Features.Repositories.Addresses.GetAllTemporarilyAddresses;
using BookShop.Data.Features.Repositories.Addresses.GetAllWardsByDistrictName;
using BookShop.Data.Features.Repositories.Addresses.RemoveAddressPermanentlyRemovedById;
using BookShop.Data.Features.Repositories.Addresses.RemoveAddressTemporarilyRemovedById;
using BookShop.Data.Features.Repositories.Addresses.RestoreAddressById;
using BookShop.Data.Features.Repositories.Addresses.UpdateAddressById;

namespace BookShop.Data.Features.Repositories.Address;

/// <summary>
///     Interface for address repository manager.
/// </summary>
public interface IAddressFeatureRepository
{
    /// <summary>
    ///     Gets get all adresses by ward name repository.
    /// </summary>
    public IGetAddressesByWardRepository GetAddressesByWardRepository { get; }

    /// <summary>
    ///     Gets get all district by province name repository.
    /// </summary>
    public IGetAllDistrictsByProvinceNameRepository GetAllDistrictsByProvinceNameRepository { get; }

    /// <summary>
    ///     Gets get all wards by district name repository.
    /// </summary>
    public IGetAllWardsByDistrictNameRepository GetAllWardsByDistrictNameRepository { get; }

    /// <summary>
    ///     Gets update address by id repository.
    /// </summary>
    public IUpdateAddressByIdRepository UpdateAddressByIdRepository { get; }

    /// <summary>
    ///     Remove address temporarily removed by id repository.
    /// </summary>
    public IRemoveAddressTemporarilyRemovedByIdRepository RemoveAddressTemporarilyRemovedByIdRepository { get; }

    /// <summary>
    ///     Remove address temporarily removed by id repository.
    /// </summary>
    public IRemoveAddressPermanentlyRemovedByIdRepository RemoveAddressPermanentlyRemovedByIdRepository { get; }

    /// <summary>
    ///     Remove restore address by id repository.
    /// </summary>
    public IRestoreAddressByIdRepository RestoreAddressByIdRepository { get; }

    /// <summary>
    ///     Get all temporarily address by id repository.
    /// </summary>
    public IGetAllTemporarilyAddressesRepository GetAllTemporarilyAddressesRepository { get; }
}
