using System;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "Payments" table.
/// </summary>
public class Payment : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary key.
    public Guid Id { get; set; }

    // Normal properties.
    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; }

    public PaymentMethod Method { get; set; }

    public DateTime PaymentDate { get; set; }

    public string TransactionId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign key.
    public Guid OrderId { get; set; }

    // Navigation properties.
    public Order Order { get; set; }

    public static class MetaData
    {
        public static class Amount
        {
            public const decimal MinValue = 0;
        }

        public static class TransactionId
        {
            public const int MaxLength = 50;
        }
    }
}

public enum PaymentStatus
{
    Pending,
    Completed,
}

public enum PaymentMethod
{
    Cash,
    BankTransfer
}
