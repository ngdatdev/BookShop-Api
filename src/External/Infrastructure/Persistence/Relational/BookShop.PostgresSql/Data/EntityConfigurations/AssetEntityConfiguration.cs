using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Asset" table.
/// </summary>
internal class AssetEntityConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable(
            name: $"{nameof(Asset)}s",
            buildAction: table => table.HasComment(comment: "Contain asset records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: asset => asset.Id);

        // ImageUrl property configuration
        builder
            .Property(propertyExpression: asset => asset.ImageUrl)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Asset.MetaData.ImageUrl.MaxLength
                )
            )
            .IsRequired();
        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: asset => asset.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: asset => asset.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: asset => asset.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: asset => asset.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: asset => asset.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: asset => asset.RemovedBy).IsRequired();
    }
}
