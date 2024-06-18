using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Product.CreateProduct;

/// <summary>
///     Interface for Command CreateProductRepository
/// </summary>
public partial interface ICreateProductRepository
{
    Task<bool> CreateProductCommandAsync(
        Data.Shared.Entities.Product product,
        CancellationToken cancellationToken
    );
}
