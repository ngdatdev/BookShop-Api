using System;
using System.Collections.Generic;
using BookShop.Data.Shared.Entities.Base;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "UserDetails" table.
/// </summary>
public class UserDetail : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary key.
    // Foreign key.
    public Guid UserId { get; set; }

    // Normal properties.
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string AvatarUrl { get; set; }

    public string Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign key.
    public Guid AddressId { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public Address Address { get; set; }

    public Cart Cart { get; set; }

    // Navigation collections.
    public IEnumerable<RefreshToken> RefreshTokens { get; set; }

    public IEnumerable<Review> Reviews { get; set; }

    public IEnumerable<Order> Orders { get; set; }

    public static class MetaData
    {
        public static class LastName
        {
            public const int MinLength = 2;

            public const int MaxLength = 50;
        }

        public static class FirstName
        {
            public const int MinLength = 2;

            public const int MaxLength = 50;
        }

        public static class AvatarUrl
        {
            public const int MinLength = 2;

            public const int MaxLength = 300;
        }

        public static class BackgroundUrl
        {
            public const int MinLength = 2;
        }

        public static class Gender
        {
            public const int MinLength = 2;

            public const int MaxLength = 10;
        }
    }
}
