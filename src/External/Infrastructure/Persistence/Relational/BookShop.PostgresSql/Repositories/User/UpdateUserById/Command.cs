using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.UpdateUserById;

/// <summary>
///    Implement of command IUpdateUserByIdRepository.
/// </summary>
internal partial class UpdateUserByIdRepository
{
    public async Task<bool> UpdateUserByIdCommandAsync(
        BookShop.Data.Shared.Entities.User updateUser,
        BookShop.Data.Shared.Entities.User currentUser,
        CancellationToken cancellationToken
    )
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                var updateUserDetailEntry = _userDetails.Entry(entity: updateUser.UserDetail);
                var currentUserDetailEntry = _userDetails.Entry(entity: currentUser.UserDetail);

                updateUserDetailEntry.State = EntityState.Unchanged;

                foreach (var property in updateUserDetailEntry.Properties)
                {
                    if (
                        !property.Metadata.IsPrimaryKey()
                        && !Equals(
                            objA: property.CurrentValue,
                            objB: currentUserDetailEntry
                                .Property(propertyName: property.Metadata.Name)
                                .CurrentValue
                        )
                    )
                    {
                        property.IsModified = true;
                    }
                }

                //var updateUserEntry = _users.Entry(entity: updateUser);
                //var currentUserEntry = _users.Entry(entity: currentUser);

                //updateUserEntry.State = EntityState.Unchanged;

                //foreach (var property in updateUserEntry.Properties)
                //{
                //    if (
                //        !property.Metadata.IsPrimaryKey()
                //        && !Equals(
                //            objA: property.CurrentValue,
                //            objB: currentUserEntry
                //                .Property(propertyName: property.Metadata.Name)
                //                .CurrentValue
                //        )
                //    )
                //    {
                //        property.IsModified = true;
                //    }
                //}

                using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                //try
                //{
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                dbTransactionResult = true;
                //}
                //catch
                //{
                //    dbTransactionResult = false;
                //}
            });

        return dbTransactionResult;
    }

    public async Task<bool> CreateAddressCommandAsync(
        BookShop.Data.Shared.Entities.Address address,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _addresses.AddAsync(entity: address, cancellationToken: cancellationToken);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
}
