using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represent "Users" table configuration.
/// </summary>
internal sealed class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        const string TableName = "Payments";
        const string TableComment = "Contain payment record.";

        builder.ToTable(name: TableName, buildAction: table => table.HasComment(TableComment));

        // Primary key configuration.
        builder.HasKey(keyExpression: payment => payment.Id);

        // Amount property configuration
        builder
            .Property(propertyExpression: payment => payment.Amount)
            .HasColumnType(typeName: CommonConstant.DataType.MONEY)
            .IsRequired();

        // Status property configuration
        builder
            .Property(propertyExpression: payment => payment.Status)
            .HasConversion<string>()
            .IsRequired();

        // Method property configuration
        builder
            .Property(propertyExpression: payment => payment.Method)
            .HasConversion<string>()
            .IsRequired();

        // TransactionId property configuration
        builder
            .Property(propertyExpression: payment => payment.TransactionId)
            .HasColumnType(typeName: CommonConstant.DataType.VarcharGenerator.Get(length: 10))
            .IsRequired();

        // PaymentDate property configuration.
        builder
            .Property(propertyExpression: payment => payment.PaymentDate)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: payment => payment.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: payment => payment.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: payment => payment.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: payment => payment.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: payment => payment.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: payment => payment.RemovedBy).IsRequired();
    }
}
