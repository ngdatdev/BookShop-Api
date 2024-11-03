using BookShop.Data.Shared.Entities;
using BookShop.SqlServer.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represent "UsersDetail" table configuration.
/// </summary>
internal sealed class UserDetailEntityConfiguration : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        const string TableName = "UserDetails";
        const string TableComment = "Contain user record.";

        builder.ToTable(
            name: TableName,
            buildAction: table => table.HasComment(comment: TableComment)
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: userDetail => userDetail.UserId);

        // LastName property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.LastName)
            .HasColumnType(
                typeName: CommonConstant.SqlDatabase.DataType.NvarcharGenerator.Get(
                    length: UserDetail.MetaData.LastName.MaxLength
                )
            )
            .IsRequired();

        // FirstName property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.FirstName)
            .HasColumnType(
                typeName: CommonConstant.SqlDatabase.DataType.NvarcharGenerator.Get(
                    length: UserDetail.MetaData.LastName.MaxLength
                )
            )
            .IsRequired();

        // AvatarUrl property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.AvatarUrl)
            .HasColumnType(
                typeName: CommonConstant.SqlDatabase.DataType.NvarcharGenerator.Get(
                    length: UserDetail.MetaData.AvatarUrl.MaxLength
                )
            )
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.CreatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: userDetail => userDetail.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: userDetail => userDetail.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.RemovedAt)
            .HasColumnType(typeName: CommonConstant.SqlDatabase.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: userDetail => userDetail.RemovedBy).IsRequired();

        // Relationship configurations.
        builder
            .HasMany(navigationExpression: userDetail => userDetail.RefreshTokens)
            .WithOne(navigationExpression: refreshToken => refreshToken.UserDetail)
            .HasForeignKey(foreignKeyExpression: refreshToken => refreshToken.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        builder
            .HasMany(navigationExpression: userDetail => userDetail.Reviews)
            .WithOne(navigationExpression: review => review.UserDetail)
            .HasForeignKey(foreignKeyExpression: review => review.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        builder
            .HasMany(navigationExpression: userDetail => userDetail.Orders)
            .WithOne(navigationExpression: orders => orders.UserDetail)
            .HasForeignKey(foreignKeyExpression: orders => orders.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
