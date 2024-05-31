using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.DataAccess.Data;
using BookShop.DataAccess.Entities;
using BookShop.DataAccess.Repositories.Concrete.Base;
using BookShop.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DataAccess.Repositories.Concrete;

/// <summary>
///     Implementation of refreshToken repository.
/// </summary>
internal sealed class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    private readonly BookShopContext _context;
    private readonly DbSet<RefreshToken> _refreshTokens;

    internal RefreshTokenRepository(BookShopContext context)
        : base(context)
    {
        _context = context;
        _refreshTokens = _context.Set<RefreshToken>();
    }

    public async Task<bool> IsRefreshTokenFoundByAccessTokenIdAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    )
    {
        return await _refreshTokens.AnyAsync(
            predicate: refreshToken => refreshToken.AccessTokenId == accessTokenId,
            cancellationToken: cancellationToken
        );
    }

    public async Task<bool> CreateRefreshTokenAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _context
                .Set<RefreshToken>()
                .AddAsync(entity: refreshToken, cancellationToken: cancellationToken);

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
}
