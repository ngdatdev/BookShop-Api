using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Data;
using BookShop.Data.Entities;
using BookShop.Data.Repositories.Concrete.Base;
using BookShop.Data.Repositories.Interface;
using BookShop.Shared.Constant;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Repositories.Concrete;

/// <summary>
///     Implementation of user detail repository.
/// </summary>
internal sealed class UserDetailRepository : BaseRepository<UserDetail>, IUserDetailRepository
{
    private readonly BookShopContext _context;
    private readonly DbSet<UserDetail> _userDetails;

    internal UserDetailRepository(BookShopContext context)
        : base(context)
    {
        _context = context;
        _userDetails = _context.Set<UserDetail>();
    }

    public Task<UserDetail> GetUserDetailByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails
            .AsNoTracking()
            .Where(userDetail => userDetail.UserId == userId)
            .Select(userDetail => new UserDetail()
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                AvatarUrl = userDetail.AvatarUrl,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserTemporarilyRemovedAsync(Guid id, CancellationToken cancellationToken)
    {
        return _userDetails.AnyAsync(
            predicate: userDetail =>
                userDetail.UserId == id
                && userDetail.RemovedBy != DefaultGuid.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != DateTime.MinValue,
            cancellationToken: cancellationToken
        );
    }
}
