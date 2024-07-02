using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Orders.CreateOrder;

/// <summary>
///     Interface for Query CreateOrder Repository
/// </summary>
public partial interface ICreateOrderRepository
{
    Task<bool> CreateOrderCommandAsync(Order order, CancellationToken cancellationToken);
    Task<bool> CreateAddressCommandAsync(
        Shared.Entities.Address address,
        CancellationToken cancellationToken
    );
}
