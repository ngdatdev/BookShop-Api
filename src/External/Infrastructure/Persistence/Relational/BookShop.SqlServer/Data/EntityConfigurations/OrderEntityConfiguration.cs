using BookShop.Data.Shared.Entities;
using BookShop.SqlServer.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.SqlServer.Data.EntityConfigurations;

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
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.MONEY)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: order => order.CreatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: order => order.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: order => order.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: order => order.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: order => order.RemovedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: order => order.RemovedBy).IsRequired();

        // Relationship configurations.
        builder
            .HasMany(order => order.OrderDetails)
            .WithOne(orderDetail => orderDetail.Order)
            .HasForeignKey(orderDetail => orderDetail.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
