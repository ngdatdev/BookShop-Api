using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.PostgresSql.Repositories.Product.UpdateProductById;

/// <summary>
///    Implement of command IUpdateProductByIdRepository.
/// </summary>
internal partial class UpdateProductByIdRepository
{
    public async Task<bool> UpdateProductByIdCommandAsync(
        BookShop.Data.Shared.Entities.Product updateProduct,
        BookShop.Data.Shared.Entities.Product currentProduct,
        CancellationToken cancellationToken
    )
    {
        var updateTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                // Asset of product table database operation.
                #region Asset
                var currentSubUrls = currentProduct.Assets.Select(asset => asset.ImageUrl);
                var updateSubUrls = updateProduct.Assets.Select(asset => asset.ImageUrl);

                var removedSubUrls = currentSubUrls.Except(
                    second: updateSubUrls,
                    comparer: StringComparer.OrdinalIgnoreCase
                );

                if (!removedSubUrls.IsNullOrEmpty())
                {
                    await _assets
                        .Where(predicate: asset =>
                            asset.ProductId == currentProduct.Id
                            && removedSubUrls.Contains(asset.ImageUrl)
                        )
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);
                }

                var addSubUrls = updateSubUrls.Except(
                    second: currentSubUrls,
                    comparer: StringComparer.OrdinalIgnoreCase
                );

                if (!addSubUrls.IsNullOrEmpty())
                {
                    await _assets.AddRangeAsync(entities: updateProduct.Assets);
                }
                #endregion

                // Category table database operation.
                #region Category
                var currentCategory = currentProduct.ProductCategories.Select(
                    selector: productCategory => productCategory.CategoryId
                );
                var updateCategory = updateProduct.ProductCategories.Select(
                    selector: productCategory => productCategory.CategoryId
                );

                var removedCategories = currentCategory.Except(second: updateCategory);
                if (!removedCategories.IsNullOrEmpty())
                {
                    await _productCategories
                        .Where(predicate: productCategory =>
                            productCategory.ProductId == currentProduct.Id
                            && removedCategories.Contains(productCategory.CategoryId)
                        )
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);
                }

                var addCategories = updateCategory.Except(second: currentCategory);

                if (!addCategories.IsNullOrEmpty())
                {
                    await _productCategories.AddRangeAsync(
                        entities: updateProduct.ProductCategories,
                        cancellationToken: cancellationToken
                    );
                }
                #endregion

                #region Product
                var updateProductEntry = _products.Entry(entity: updateProduct);
                var currentProductEntry = _products.Entry(entity: currentProduct);

                updateProductEntry.State = EntityState.Unchanged;

                foreach (var property in updateProductEntry.Properties)
                {
                    if (
                        !property.Metadata.IsPrimaryKey()
                        && !Equals(
                            objA: property.CurrentValue,
                            objB: currentProductEntry
                                .Property(propertyName: property.Metadata.Name)
                                .OriginalValue
                        )
                    )
                    {
                        property.IsModified = true;
                    }
                }

                #endregion


                try
                {
                    using var dbTransaction = await _context.Database.BeginTransactionAsync(
                        cancellationToken: cancellationToken
                    );

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    updateTransactionResult = true;
                }
                catch
                {
                    updateTransactionResult = false;
                }
            });

        return updateTransactionResult;
    }
}
