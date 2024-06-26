using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "OrderDetails" table.
/// </summary>
internal sealed class OrderDetailDetailEntityConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable(
            name: $"{nameof(OrderDetail)}s",
            buildAction: table => table.HasComment(comment: "Contain OrderDetail records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: orderDetail => orderDetail.Id);

        // Quantity property configuration
        builder.Property(propertyExpression: orderDetail => orderDetail.Quantity).IsRequired();

        // TotalCost property configuration
        builder
            .Property(propertyExpression: orderDetail => orderDetail.Cost)
            .HasColumnType(typeName: CommonConstant.DataType.MONEY)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: orderDetail => orderDetail.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: orderDetail => orderDetail.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: orderDetail => orderDetail.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: orderDetail => orderDetail.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: orderDetail => orderDetail.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: orderDetail => orderDetail.RemovedBy).IsRequired();
    }
}
