using BookShop.Data.Entities;
using BookShop.Data.UnitOfWork;
using BookShop.SqlServer.Data;
using Microsoft.AspNetCore.Identity;

namespace BookShop.SqlServer.UnitOfWorks;

/// <summary>
///     Implementation of unit of work interface.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly BookShopContext _context;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public UnitOfWork(
        BookShopContext context,
        RoleManager<Role> roleManager,
        UserManager<User> userManager
    )
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }
}
