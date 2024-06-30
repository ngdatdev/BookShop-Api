using BookShop.Data.Features.Repositories.Auth.ResendUserRegistrationConfirmedEmail;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.ResendUserRegistrationConfirmedEmail;

/// <summary>
///    Implement of IResendUserRegistrationConfirmedEmail repository.
/// </summary>
internal partial class ResendUserRegistrationConfirmedEmailRepository
    : IResendUserRegistrationConfirmedEmailRepository
{
    private readonly BookShopContext _context;
    private DbSet<UserDetail> _usersDetail;

    public ResendUserRegistrationConfirmedEmailRepository(BookShopContext context)
    {
        _context = context;
        _usersDetail = _context.Set<UserDetail>();
    }
}
