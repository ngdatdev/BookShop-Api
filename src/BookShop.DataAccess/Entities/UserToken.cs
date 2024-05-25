using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.DataAccess.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace BookShop.DataAccess.Entities;

/// <summary>
///     Represent the "UserTokens" table.
/// </summary>
public class UserToken : IdentityUserToken<Guid>, IBaseEntity
{
    public DateTime ExpiredAt { get; set; }

    // Navigation properties.
    public User User { get; set; }
}
