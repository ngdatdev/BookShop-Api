using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Address" table.
/// </summary>
internal sealed class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(
            name: $"{nameof(Address)}es",
            buildAction: table => table.HasComment(comment: "Contain address records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: address => address.Id);

        // Ward property configuration
        builder
            .Property(propertyExpression: address => address.Ward)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Address.MetaData.Ward.MaxLength
                )
            )
            .IsRequired();

        // District property configuration
        builder
            .Property(propertyExpression: address => address.District)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Address.MetaData.District.MaxLength
                )
            )
            .IsRequired();

        // Province property configuration
        builder
            .Property(propertyExpression: address => address.Province)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Address.MetaData.Province.MaxLength
                )
            )
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: address => address.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: address => address.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: address => address.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: address => address.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: address => address.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: address => address.RemovedBy).IsRequired();

        // Relationship configurations.
        builder
            .HasMany(navigationExpression: address => address.UserDetails)
            .WithOne(navigationExpression: userDetail => userDetail.Address)
            .HasForeignKey(foreignKeyExpression: userDetail => userDetail.AddressId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        builder
            .HasMany(navigationExpression: address => address.Orders)
            .WithOne(navigationExpression: order => order.Address)
            .HasForeignKey(foreignKeyExpression: order => order.AddressId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
