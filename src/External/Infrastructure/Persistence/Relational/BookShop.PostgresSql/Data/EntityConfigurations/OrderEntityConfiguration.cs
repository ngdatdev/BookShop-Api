using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Orders" table.
/// </summary>
internal sealed class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(
            name: $"{nameof(Order)}s",
            buildAction: table => table.HasComment(comment: "Contain Order records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: order => order.Id);

        // TotalCost property configuration
        builder
            .Property(propertyExpression: order => order.TotalCost)
            .HasColumnType(typeName: CommonConstant.DataType.MONEY)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: order => order.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: order => order.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: order => order.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: order => order.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: order => order.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: order => order.RemovedBy).IsRequired();

        // Relationship configurations.
        // [Order] - [OrderDetail] (1 - n)
        builder
            .HasMany(order => order.OrderDetails)
            .WithOne(orderDetail => orderDetail.Order)
            .HasForeignKey(orderDetail => orderDetail.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // [Order] - [Payment] (1 - 1).
        builder
            .HasOne(order => order.Payment)
            .WithOne(payment => payment.Order)
            .HasForeignKey<Payment>(payment => payment.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
