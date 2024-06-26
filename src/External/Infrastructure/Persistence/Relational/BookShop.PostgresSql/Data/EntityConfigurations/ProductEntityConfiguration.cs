using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Products" table.
/// </summary>
internal sealed class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(
            name: $"{nameof(Product)}s",
            buildAction: table => table.HasComment(comment: "Contain Product records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: product => product.Id);

        // FullName property configuration
        builder
            .Property(propertyExpression: product => product.FullName)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Product.MetaData.FullName.MaxLength
                )
            )
            .IsRequired();

        // Description property configuration
        builder
            .Property(propertyExpression: product => product.Description)
            .HasColumnType(typeName: CommonConstant.DataType.TEXT)
            .IsRequired();

        // Price property configuration
        builder
            .Property(propertyExpression: product => product.Price)
            .HasColumnType(typeName: CommonConstant.DataType.MONEY)
            .IsRequired();

        // Discount property configuration
        builder
            .Property(propertyExpression: product => product.Discount)
            .HasDefaultValue(value: default)
            .IsRequired();

        // Size property configuration
        builder.Property(propertyExpression: product => product.Size).IsRequired();

        // NumberOfPage property configuration
        builder.Property(propertyExpression: product => product.NumberOfPage).IsRequired();

        // Author property configuration
        builder
            .Property(propertyExpression: product => product.Author)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Product.MetaData.Author.MaxLength
                )
            )
            .IsRequired();

        // Author property configuration
        builder
            .Property(propertyExpression: product => product.Languages)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Product.MetaData.Languages.MaxLength
                )
            )
            .IsRequired();

        // Publisher property configuration
        builder
            .Property(propertyExpression: product => product.Publisher)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Product.MetaData.Publisher.MaxLength
                )
            )
            .IsRequired();

        // QuantityCurrent property configuration
        builder.Property(propertyExpression: product => product.QuantityCurrent).IsRequired();

        // QuantitySold property configuration
        builder.Property(propertyExpression: product => product.QuantitySold).IsRequired();

        // QuantitySold property configuration
        builder
            .Property(propertyExpression: product => product.ImageUrl)
            .HasColumnType(typeName: CommonConstant.DataType.TEXT)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: product => product.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: product => product.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: product => product.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: product => product.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: product => product.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: product => product.RemovedBy).IsRequired();

        // Relationship configurations.
        // [Product] - [Reviews] (1 - N).
        builder
            .HasMany(product => product.Reviews)
            .WithOne(review => review.Product)
            .HasForeignKey(review => review.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        // [Product] - [OrderDetails] (1 - N).
        builder
            .HasMany(product => product.OrderDetails)
            .WithOne(orderDetail => orderDetail.Product)
            .HasForeignKey(orderDetail => orderDetail.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        // [Product] - [CartItems] (1 - N).
        builder
            .HasMany(product => product.CartItems)
            .WithOne(cartItem => cartItem.Product)
            .HasForeignKey(cartItem => cartItem.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // [Product] - [ProductCategory] (1 - N).
        builder
            .HasMany(product => product.ProductCategories)
            .WithOne(productCategory => productCategory.Product)
            .HasForeignKey(productCategory => productCategory.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // [Product] - [Assets] (1 - N).
        builder
            .HasMany(product => product.Assets)
            .WithOne(asset => asset.Product)
            .HasForeignKey(asset => asset.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
