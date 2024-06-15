using System;
using System.Collections.Generic;
using BookShop.Data.Shared.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "Users" table.
/// </summary>
public class User : IdentityUser<Guid>, IBaseEntity
{
    // Navigation properties.
    public UserDetail UserDetail { get; set; }

    // Navigation collections.
    public IEnumerable<UserRole> UserRoles { get; set; }

    // Navigation collections.
    public IEnumerable<UserToken> UserTokens { get; set; }

    // Additional information of this table.
    public static class MetaData
    {
        public static class Email
        {
            public const int MaxLength = 256;

            public const int MinLength = 2;
        }

        public static class UserName
        {
            public const int MaxLength = 256;

            public const int MinLength = 2;
        }

        public static class Password
        {
            public const int MinLength = 4;

            public const int MaxLength = 256;
        }
    }
}
