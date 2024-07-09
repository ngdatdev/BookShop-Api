using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.CreateProduct;

/// <summary>
///     Interface for Command CreateProduct Repository
/// </summary>
public partial interface ICreateProductRepository
{
    Task<bool> CreateProductCommandAsync(
        Data.Shared.Entities.Product product,
        CancellationToken cancellationToken
    );
}
