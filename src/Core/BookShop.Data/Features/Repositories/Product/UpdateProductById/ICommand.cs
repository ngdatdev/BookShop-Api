using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.UpdateProductById;

/// <summary>
///     Interface for Command UpdateProductByIdRepository
/// </summary>
public partial interface IUpdateProductByIdRepository
{
    Task<bool> UpdateProductByIdCommandAsync(
        Shared.Entities.Product updateProduct,
        Shared.Entities.Product currentProduct,
        CancellationToken cancellationToken
    );
}
