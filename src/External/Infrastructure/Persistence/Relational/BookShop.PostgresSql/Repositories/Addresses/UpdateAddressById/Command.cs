using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.UpdateAddressById;

/// <summary>
///    Implement of command IUpdateAddressById repository.
/// </summary>
internal partial class UpdateAddressByIdRepository
{
    public async Task<bool> UpdateAddressByIdCommandAsync(
        BookShop.Data.Shared.Entities.Address currentAddress,
        BookShop.Data.Shared.Entities.Address updateAddress,
        CancellationToken cancellationToken
    )
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                var currentAddressEntry = _addresses.Entry(entity: currentAddress);
                var updateAddressEntry = _addresses.Entry(entity: updateAddress);

                updateAddressEntry.State = EntityState.Unchanged;

                foreach (var property in updateAddressEntry.Properties)
                {
                    if (
                        !property.Metadata.IsPrimaryKey()
                        && !Equals(
                            objA: property.CurrentValue,
                            objB: currentAddressEntry
                                .Property(propertyName: property.Metadata.Name)
                                .CurrentValue
                        )
                    )
                    {
                        property.IsModified = true;
                    }
                }

                try
                {
                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return dbTransactionResult;
    }
}
