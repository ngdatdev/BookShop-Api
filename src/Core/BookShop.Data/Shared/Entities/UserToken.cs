using System;
using BookShop.Data.Shared.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "UserTokens" table.
/// </summary>
public class UserToken : IdentityUserToken<Guid>, IBaseEntity
{
    public DateTime ExpiredAt { get; set; }

    // Navigation properties.
    public User User { get; set; }
}
