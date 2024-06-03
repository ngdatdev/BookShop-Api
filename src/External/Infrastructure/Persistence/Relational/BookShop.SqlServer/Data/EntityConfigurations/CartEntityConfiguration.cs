using BookShop.Data.Shared.Entities;
using BookShop.SqlServer.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Carts" table.
/// </summary>
internal sealed class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable(
            name: $"{nameof(Cart)}s",
            buildAction: table => table.HasComment(comment: "Contain cart records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: cart => cart.Id);

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: cart => cart.CreatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: cart => cart.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: cart => cart.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: cart => cart.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: cart => cart.RemovedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: cart => cart.RemovedBy).IsRequired();

        // Relationship configurations.
        builder
            .HasMany(cart => cart.CartItems)
            .WithOne(cart => cart.Cart)
            .HasForeignKey(cart => cart.CartId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
