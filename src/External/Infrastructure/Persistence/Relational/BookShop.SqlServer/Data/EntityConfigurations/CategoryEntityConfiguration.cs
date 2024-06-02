using BookShop.Data.Entities;
using BookShop.SqlServer.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Categories" table.
/// </summary>
internal sealed class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(
            name: $"{nameof(Category)}s",
            buildAction: table => table.HasComment(comment: "Contain Category records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: category => category.Id);

        // Description property configuration
        builder
            .Property(propertyExpression: category => category.Description)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.NVARCHAR_MAX)
            .IsRequired();

        // FullName property configuration
        builder
            .Property(propertyExpression: category => category.FullName)
            .HasColumnType(
                typeName: CommonConstant.SqlDatabase.DataType.NvarcharGenerator.Get(
                    length: Category.MetaData.FullName.MaxLength
                )
            )
            .IsRequired();

        // Relationship configurations.
        builder
            .HasMany(category => category.Products)
            .WithOne(product => product.Category)
            .HasForeignKey(product => product.CategoryId);
    }
}
