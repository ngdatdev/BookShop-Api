using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Data;

/// <summary>
///     Represent data seeding for database.
/// </summary>
public static class EntityDataSeeding
{
    private static readonly Guid AdminId = Guid.Parse(
        input: "2ed6fb47-86a6-46e3-a9dc-ff5b3d986c2f"
    );

    /// <summary>
    ///     Seed data.
    /// </summary>
    /// <param name="context">
    ///     Database context for interacting with other table.
    /// </param>
    /// <param name="userManager">
    ///     Specific manager for interacting with user table.
    /// </param>
    /// <param name="roleManager">
    ///     Specific manager for interacting with role table.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if seeding is success. Otherwise, false.
    /// </returns>
    public static async Task<bool> SeedAsync(
        BookShopContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        CancellationToken cancellationToken
    )
    {
        var userDetails = context.Set<UserDetail>();
        var roles = context.Set<Role>();
        var orderStatuses = context.Set<OrderStatus>();
        var addresses = context.Set<Address>();

        // Is tables empty.
        var isTableEmpty = await IsTableEmptyAsync(
            userDetails: userDetails,
            roles: roles,
            orderStatuses: orderStatuses,
            addresses: addresses,
            cancellationToken: cancellationToken
        );

        if (!isTableEmpty)
        {
            return true;
        }

        // Init list of address.
        var newAddresses = InitAddresses();

        // Init list of role.
        var newRoles = InitNewRoles();

        // Init admin.
        var admin = InitAdmin();

        #region Transaction
        var executedTransactionResult = false;

        await context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var dbTransaction = await context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await addresses.AddRangeAsync(
                        entities: newAddresses,
                        cancellationToken: cancellationToken
                    );

                    foreach (var newRole in newRoles)
                    {
                        await roleManager.CreateAsync(role: newRole);
                    }

                    await userManager.CreateAsync(user: admin, password: "Zxcl123123@");

                    await userManager.AddToRoleAsync(user: admin, role: "admin");

                    var emailConfirmationToken =
                        await userManager.GenerateEmailConfirmationTokenAsync(user: admin);

                    await userManager.ConfirmEmailAsync(user: admin, token: emailConfirmationToken);

                    await context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        #endregion

        return executedTransactionResult;
    }

    private static async Task<bool> IsTableEmptyAsync(
        DbSet<UserDetail> userDetails,
        DbSet<Role> roles,
        DbSet<OrderStatus> orderStatuses,
        DbSet<Address> addresses,
        CancellationToken cancellationToken
    )
    {
        // Is user details table empty.
        var isTableNotEmpty = await userDetails.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        // Is roles table empty.
        isTableNotEmpty = await roles.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        // Is orderStatuses table empty.
        isTableNotEmpty = await orderStatuses.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        // Is addresses table empty.
        isTableNotEmpty = await addresses.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        return true;
    }

    private static List<Role> InitNewRoles()
    {
        Dictionary<Guid, string> newRoleNames = [];

        Guid userRole = Guid.Parse(input: "c39aa1ac-8ded-46be-870c-115b200b09fc");
        Guid adminRole = Guid.Parse(input: "c8500b46-b134-4b60-85b7-8e6af1187e0c");

        newRoleNames.Add(key: userRole, value: "user");

        newRoleNames.Add(key: adminRole, value: "admin");

        List<Role> newRoles = [];

        foreach (var newRoleName in newRoleNames)
        {
            Role newRole =
                new()
                {
                    Id = newRoleName.Key,
                    Name = newRoleName.Value,
                    RoleDetail = new()
                    {
                        RoleId = newRoleName.Key,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = AdminId,
                        UpdatedAt = CommonConstant.MIN_DATE_TIME,
                        UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                        RemovedAt = CommonConstant.MIN_DATE_TIME,
                        RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    }
                };

            newRoles.Add(item: newRole);
        }

        return newRoles;
    }

    private static List<Address> InitAddresses()
    {
        List<string> newAddresses = ["Thanh Thủy, Lệ Thủy, Quảng Bình",];

        List<Address> addresses = [];

        foreach (var address in newAddresses)
        {
            string[] addressParts = address.Split(", ");

            addresses.Add(
                new()
                {
                    Id = Guid.Parse(input: "37777b21-e6d1-4e54-9067-407b7bd65774"),
                    Ward = addressParts[0],
                    District = addressParts[1],
                    Province = addressParts[2],
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = AdminId,
                    RemovedAt = CommonConstant.MIN_DATE_TIME,
                    RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    UpdatedAt = CommonConstant.MIN_DATE_TIME,
                    UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                }
            );
        }
        return addresses;
    }

    private static User InitAdmin()
    {
        User admin =
            new()
            {
                Id = AdminId,
                UserName = "admin",
                Email = "nvdatdz8b@gmail.com",
                UserDetail = new()
                {
                    UserId = AdminId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = AdminId,
                    RemovedAt = CommonConstant.MIN_DATE_TIME,
                    RemovedBy = Guid.Parse(input: "c8500b46-b134-4b60-85b7-8e6af1187e1c"),
                    UpdatedAt = CommonConstant.MIN_DATE_TIME,
                    UpdatedBy = Guid.Parse(input: "c8500b46-b134-4b60-85b7-8e6af1187e1c"),
                    FirstName = "Nguyen",
                    LastName = "Dat",
                    AvatarUrl = "url.com/img",
                    AddressId = Guid.Parse(input: "37777b21-e6d1-4e54-9067-407b7bd65774"),
                }
            };

        return admin;
    }
}
