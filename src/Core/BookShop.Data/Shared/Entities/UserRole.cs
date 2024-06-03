using System;
using BookShop.Data.Shared.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Data.Shared.Entities;

/// <summary>
///     Represent the "UserRoles" table.
/// </summary>
public class UserRole : IdentityUserRole<Guid>, IBaseEntity
{
    // Navigation properties.
    public User User { get; set; }

    public Role Role { get; set; }
}
