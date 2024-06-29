using BookShop.Data.Features.Repositories.Auth.ConfirmUserRegistrationEmail;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.ConfirmUserRegistrationEmail;

/// <summary>
///    Implement of IConfirmUserRegistrationEmail repository.
/// </summary>
internal partial class ConfirmUserRegistrationEmailRepository
    : IConfirmUserRegistrationEmailRepository
{
    private readonly BookShopContext _context;
    private DbSet<UserDetail> _usersDetail;

    public ConfirmUserRegistrationEmailRepository(BookShopContext context)
    {
        _context = context;
        _usersDetail = _context.Set<UserDetail>();
    }
}
