using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "CartItems" table.
/// </summary>
internal sealed class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable(
            name: $"{nameof(CartItem)}s",
            buildAction: table => table.HasComment(comment: "Contain CartItem records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: cartItem => cartItem.Id);

        // Quantity property configuration
        builder.Property(propertyExpression: cartItem => cartItem.Quantity).IsRequired();

        // TotalCost property configuration
        builder
            .Property(propertyExpression: cartItem => cartItem.TotalCost)
            .HasColumnType(CommonConstant.DataType.MONEY)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: cartItem => cartItem.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: cartItem => cartItem.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: cartItem => cartItem.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: cartItem => cartItem.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: cartItem => cartItem.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: cartItem => cartItem.RemovedBy).IsRequired();
    }
}
