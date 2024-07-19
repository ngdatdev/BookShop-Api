using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.CreateOrder;

/// <summary>
///    Implement of query ICreateOrder repository.
/// </summary>
internal partial class CreateOrderRepository
{
    public Task<Guid> FindAddressIdFoundByNameQueryAsync(
        string ward,
        string district,
        string province,
        CancellationToken cancellationToken
    )
    {
        return _addresses
            .AsNoTracking()
            .Where(predicate: address =>
                EF.Functions.Collate(
                        address.Ward,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(ward)
                && EF.Functions.Collate(
                        address.District,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(district)
                && EF.Functions.Collate(
                        address.Province,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(province)
            )
            .Select(address => address.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<
        IEnumerable<BookShop.Data.Shared.Entities.Product>
    > FindQuantityProductByIdQueryAsync(
        IEnumerable<Guid> productIds,
        CancellationToken cancellationToken
    )
    {
        return await _products
            .AsNoTracking()
            .Where(product => productIds.Contains(product.Id))
            .Select(selector: product => new BookShop.Data.Shared.Entities.Product()
            {
                Price = product.Price,
                Id = product.Id,
                Discount = product.Discount,
                QuantityCurrent = product.QuantityCurrent,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> IsProductsTemporarilyRemovedQueryAsync(
        IEnumerable<Guid> productIds,
        CancellationToken cancellationToken
    )
    {
        var matchingProductCount = await _products
            .AsNoTracking()
            .Where(product =>
                productIds.Contains(product.Id)
                && product.RemovedAt != CommonConstant.MIN_DATE_TIME
                && product.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .CountAsync(cancellationToken);

        return matchingProductCount == productIds.Count();
    }
}
