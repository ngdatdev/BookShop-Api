using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "RefreshToken" table.
/// </summary>
internal sealed class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(
            name: $"{nameof(RefreshToken)}s",
            buildAction: table => table.HasComment(comment: "Contain refresh token records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: refreshToken => refreshToken.AccessTokenId);

        // FullName property configuration
        builder
            .Property(propertyExpression: refreshToken => refreshToken.RefreshTokenValue)
            .HasColumnType(typeName: CommonConstant.DataType.TEXT)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: refreshToken => refreshToken.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // ExpiredDate property configuration.
        builder
            .Property(propertyExpression: refreshToken => refreshToken.ExpiredDate)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();
    }
}
