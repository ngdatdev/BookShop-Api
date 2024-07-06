using BookShop.Data.Features.Repositories.Addresses.GetAddressesByWard;

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
}
