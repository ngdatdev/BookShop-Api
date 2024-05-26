using System;
using BookShop.DataAccess.Constants;
using BookShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.DataAccess.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "CartItems" table.
/// </summary>
public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItem>
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
            .HasColumnType(CommonConstant.SqlDatabase.DataType.MONEY)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: cartItem => cartItem.CreatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: cartItem => cartItem.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: cartItem => cartItem.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: cartItem => cartItem.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: cartItem => cartItem.RemovedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: cartItem => cartItem.RemovedBy).IsRequired();
    }
}
