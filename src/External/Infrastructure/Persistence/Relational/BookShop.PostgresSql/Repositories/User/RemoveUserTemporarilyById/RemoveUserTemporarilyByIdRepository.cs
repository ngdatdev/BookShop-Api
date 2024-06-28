using BookShop.Data.Features.Repositories.User.RemoveUserTemporarilyById;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.RemoveUserTemporarilyById;

/// <summary>
///    Implement of IRemoveUserTemporarilyByIdRepository repository.
/// </summary>
internal partial class RemoveUserTemporarilyByIdRepository : IRemoveUserTemporarilyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.User> _users;
    private DbSet<BookShop.Data.Shared.Entities.UserDetail> _userDetails;

    public RemoveUserTemporarilyByIdRepository(BookShopContext context)
    {
        _context = context;
        _users = _context.Set<BookShop.Data.Shared.Entities.User>();
        _userDetails = _context.Set<BookShop.Data.Shared.Entities.UserDetail>();
    }
}
