using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.DataAccess.Constants;
using BookShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.DataAccess.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Categories" table.
/// </summary>
public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
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
