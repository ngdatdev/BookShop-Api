using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Reviews" table.
/// </summary>
internal sealed class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable(
            name: $"{nameof(Review)}s",
            buildAction: table => table.HasComment(comment: "Contain Review records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: review => review.Id);

        // Comment property configuration
        builder
            .Property(propertyExpression: review => review.Comment)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Review.MetaData.Comment.MaxLength
                )
            )
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: review => review.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: review => review.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: review => review.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: review => review.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: review => review.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: review => review.RemovedBy).IsRequired();
    }
}
