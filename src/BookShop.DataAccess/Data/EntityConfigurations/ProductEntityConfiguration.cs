using BookShop.DataAccess.Constants;
using BookShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.DataAccess.Data.EntityConfigurations;

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
                typeName: CommonConstant.SqlDatabase.DataType.NvarcharGenerator.Get(
                    length: Product.MetaData.FullName.MaxLength
                )
            )
            .IsRequired();

        // Description property configuration
        builder
            .Property(propertyExpression: product => product.Description)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.NVARCHAR_MAX)
            .IsRequired();

        // QuantityCurrent property configuration
        builder.Property(propertyExpression: product => product.QuantityCurrent).IsRequired();

        // QuantitySold property configuration
        builder.Property(propertyExpression: product => product.QuantitySold).IsRequired();

        // QuantitySold property configuration
        builder
            .Property(propertyExpression: product => product.ImageUrl)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.NVARCHAR_MAX)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: product => product.CreatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: product => product.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: product => product.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: product => product.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: product => product.RemovedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: product => product.RemovedBy).IsRequired();

        // Relationship configurations.
        builder
            .HasMany(product => product.Reviews)
            .WithOne(review => review.Product)
            .HasForeignKey(review => review.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(product => product.OrderDetails)
            .WithOne(orderDetail => orderDetail.Product)
            .HasForeignKey(orderDetail => orderDetail.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(product => product.CartItems)
            .WithOne(cartItem => cartItem.Product)
            .HasForeignKey(cartItem => cartItem.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
