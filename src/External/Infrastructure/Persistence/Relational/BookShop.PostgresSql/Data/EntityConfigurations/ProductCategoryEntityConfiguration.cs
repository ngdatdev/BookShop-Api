using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.PostgresSql.Constants;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "ProductCategory" table.
/// </summary>
internal class ProductCategoryEntityConfiguration
: IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable(
            name: $"{nameof(ProductCategory)}",
            buildAction: table => table.HasComment(comment: "Contain ProductCategory records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: productCategory => new
        {
            productCategory.ProductId,
            productCategory.CategoryId,
        });
    }
}