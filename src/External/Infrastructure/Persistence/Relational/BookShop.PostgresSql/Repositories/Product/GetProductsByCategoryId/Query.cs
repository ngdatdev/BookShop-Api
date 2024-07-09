using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.FilterAndPagination;
using BookShop.PostgresSql.Repositories.Product.GetProductsByCategoryId.QueryMapper;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.GetProductsByCategoryId;

/// <summary>
///    Implement of query IGetProductsByCategoryId repository.
/// </summary>
internal partial class GetProductsByCategoryIdRepository
{
    public Task<Category> FindCategoryByIdQueryAsync(
        Guid categoryId,
        CancellationToken cancellationToken
    )
    {
        return _categories
            .Where(predicate: category => category.Id == categoryId)
            .Select(selector: category => new Category()
            {
                FullName = category.FullName,
                ImageUrl = category.ImageUrl,
                Description = category.Description,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<
        IEnumerable<BookShop.Data.Shared.Entities.Product>
    > FindProductsByCategoryIdQueryAsync(
        Guid categoryId,
        FilterParameterQuery filterParameterQuery,
        CancellationToken cancellationToken
    )
    {
        var query = _products
            .AsQueryable()
            .Where(product =>
                product.ProductCategories.Any(productCategory =>
                    productCategory.CategoryId == categoryId
                )
                && product.RemovedAt == CommonConstant.MIN_DATE_TIME
                && product.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            );

        query = GetProductsByCatgoryIdQueryMapper
            .Get()
            .ApplyQuery(query: query, filterParameterQuery: filterParameterQuery);

        return await query
            .AsNoTracking()
            .Skip((filterParameterQuery.PageIndex - 1) * filterParameterQuery.PageSize)
            .Take(filterParameterQuery.PageSize)
            .Select(selector: product => new BookShop.Data.Shared.Entities.Product()
            {
                FullName = product.FullName,
                ImageUrl = product.ImageUrl,
                Author = product.Author,
                Discount = product.Discount,
                Price = product.Price,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<int> GetTotalNumberOfProductsByCategoryIdQueryAsync(
        Guid categoryId,
        CancellationToken cancellationToken
    )
    {
        return _products
            .Where(predicate: product =>
                product.ProductCategories.Any(productCategory =>
                    productCategory.CategoryId == categoryId
                )
                && product.RemovedAt == CommonConstant.MIN_DATE_TIME
                && product.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .CountAsync(cancellationToken: cancellationToken);
    }
}
