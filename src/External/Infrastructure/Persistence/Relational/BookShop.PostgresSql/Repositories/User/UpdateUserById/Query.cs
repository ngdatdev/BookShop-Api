using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.UpdateUserById;

/// <summary>
///    Implement of query IUpdateUserByIdRepository repository.
/// </summary>
internal partial class UpdateUserByIdRepository
{
    public Task<BookShop.Data.Shared.Entities.User> FindUserByIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _users
            .AsNoTracking()
            .Where(predicate: user => user.Id == userId)
            .Select(selector: user => new BookShop.Data.Shared.Entities.User()
            {
                Id = userId,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserDetail = new()
                {
                    UserId = userId,
                    FirstName = user.UserDetail.FirstName,
                    LastName = user.UserDetail.LastName,
                    AvatarUrl = user.UserDetail.AvatarUrl,
                    Gender = user.UserDetail.Gender,
                    DateOfBirth = user.UserDetail.DateOfBirth,
                    AddressId = user.UserDetail.AddressId,
                    CreatedAt = user.UserDetail.CreatedAt,
                    CreatedBy = user.UserDetail.CreatedBy,
                    RemovedAt = user.UserDetail.RemovedAt,
                    RemovedBy = user.UserDetail.UpdatedBy,
                    UpdatedAt = user.UserDetail.UpdatedAt,
                    UpdatedBy = user.UserDetail.UpdatedBy,
                }
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<Guid> FindAddressIdFoundByNameQueryAsync(
        string ward,
        string district,
        string province,
        CancellationToken cancellationToken
    )
    {
        return _addresses
            .Where(predicate: address =>
                EF.Functions.Collate(
                        address.Ward,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(ward)
                && EF.Functions.Collate(
                        address.District,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(district)
                && EF.Functions.Collate(
                        address.Province,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(province)
            )
            .Select(address => address.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserTemporarilyRemovedByIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: user =>
                    user.UserId == userId
                    && user.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && user.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
