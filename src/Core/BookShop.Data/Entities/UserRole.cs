using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Data.Entities;

/// <summary>
///     Represent the "UserRoles" table.
/// </summary>
public class UserRole : IdentityUserRole<Guid>, IBaseEntity
{
    // Navigation properties.
    public User User { get; set; }

    public Role Role { get; set; }
}
