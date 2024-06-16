using BookShop.Data.Features.Repositories.User;
using BookShop.Data.Features.Repositories.User.GetProfileUser;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.User.GetProfileUser;
using Microsoft.AspNetCore.Identity;

namespace BookShop.PostgresSql.Repositories.User;

/// <summary>
///    Implement of UserFeatureRepository interface.
/// </summary>
internal class UserFeatureRepository : IUserFeatureRepository
{
    private readonly BookShopContext _context;
    private readonly UserManager<BookShop.Data.Shared.Entities.User> _userManager;

    private IGetProfileUserRepository _geProfileUserRepository;

    internal UserFeatureRepository(
        BookShopContext context,
        UserManager<BookShop.Data.Shared.Entities.User> userManager
    )
    {
        _context = context;
        _userManager = userManager;
    }

    public IGetProfileUserRepository GetProfileUserRepository
    {
        get { return _geProfileUserRepository ??= new GetProfileUserRepository(context: _context); }
    }
}
