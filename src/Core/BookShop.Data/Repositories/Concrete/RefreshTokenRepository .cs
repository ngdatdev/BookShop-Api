using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Data;
using BookShop.Data.Entities;
using BookShop.Data.Repositories.Concrete.Base;
using BookShop.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Repositories.Concrete;

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
