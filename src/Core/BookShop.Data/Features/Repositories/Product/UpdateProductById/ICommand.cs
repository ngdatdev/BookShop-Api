using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Product.UpdateProductById;

/// <summary>
///     Interface for Command UpdateProductByIdRepository
/// </summary>
public partial interface IUpdateProductByIdRepository
{
    Task<bool> UpdateProductByIdCommandAsync(
        Data.Shared.Entities.Product updateProduct,
        Data.Shared.Entities.Product currentProduct,
        CancellationToken cancellationToken
    );
}
