using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Categories" table.
/// </summary>
internal sealed class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(
            name: $"Categories",
            buildAction: table => table.HasComment(comment: "Contain Category records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: category => category.Id);

        // Description property configuration
        builder
            .Property(propertyExpression: category => category.Description)
            .HasColumnType(typeName: CommonConstant.DataType.TEXT)
            .IsRequired();

        // FullName property configuration
        builder
            .Property(propertyExpression: category => category.FullName)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Category.MetaData.FullName.MaxLength
                )
            )
            .IsRequired();

        // ImageUrl property configuration
        builder
            .Property(propertyExpression: category => category.ImageUrl)
            .HasColumnType(
                typeName: CommonConstant.DataType.VarcharGenerator.Get(
                    length: Category.MetaData.ImageUrl.MaxLength
                )
            )
            .IsRequired();

        // Relationship configurations.

        // [Category] - [ProductCategory] (1 - N).
        builder
            .HasMany(category => category.ProductCategories)
            .WithOne(productCategory => productCategory.Category)
            .HasForeignKey(productCategory => productCategory.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        // [Category] - [Category] (1 - N).
        builder
            .HasMany(category => category.SubCategories)
            .WithOne(subCategory => subCategory.ParentCategory)
            .HasForeignKey(productCategory => productCategory.ParentCategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
