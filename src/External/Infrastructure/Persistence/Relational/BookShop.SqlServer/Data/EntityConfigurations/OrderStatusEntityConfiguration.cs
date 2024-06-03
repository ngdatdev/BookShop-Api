using BookShop.Data.Shared.Entities;
using BookShop.SqlServer.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "OrderStatus" table.
/// </summary>
internal sealed class OrderStatusEntityConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable(
            name: $"{nameof(OrderStatus)}es",
            buildAction: table => table.HasComment(comment: "Contain OrderStatus records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: orderStatus => orderStatus.Id);

        // FullName property configuration
        builder
            .Property(propertyExpression: orderStatus => orderStatus.FullName)
            .HasColumnType(
                typeName: CommonConstant.SqlDatabase.DataType.NvarcharGenerator.Get(
                    length: OrderStatus.MetaData.FullName.MaxLength
                )
            )
            .IsRequired();
    }
}
