using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        var categories = context.Set<Category>();
        var orderStatus = context.Set<OrderStatus>();

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

        // Init categories
        var newCategories = InitCategories();

        // Init orderStatus
        var newOrderStatus = InitOrderStatuses();

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

                    await orderStatus.AddRangeAsync(
                        entities: newOrderStatus,
                        cancellationToken: cancellationToken
                    );

                    await categories.AddRangeAsync(
                        entities: newCategories,
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
                    CreatedAt = DateTime.UtcNow.ToUniversalTime(),
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
                    Gender = "Nam",
                    DateOfBirth = new DateTime(2004, 2, 29, 0, 0, 0, DateTimeKind.Utc)
                }
            };

        return admin;
    }

    public static List<Category> InitCategories()
    {
        return new List<Category>
        {
            new Category
            {
                Id = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                FullName = "Default",
                Description = "Default.",
                ImageUrl = "Default",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Truyện tranh",
                Description = "Sách chứa các câu chuyện được kể bằng hình ảnh và lời thoại.",
                ImageUrl = "https://example.com/images/truyen-tranh.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Flashcard, Thẻ Học Online",
                Description = "Thẻ học tập giúp ghi nhớ kiến thức nhanh chóng.",
                ImageUrl = "https://example.com/images/flashcard.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Artbook & Sách tranh",
                Description = "Sách chứa các bức tranh nghệ thuật.",
                ImageUrl = "https://example.com/images/artbook.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Văn học",
                Description = "Sách chứa những câu chuyện hư cấu và các tác phẩm văn học.",
                ImageUrl = "https://example.com/images/van-hoc.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Kinh tế",
                Description = "Sách về các nguyên lý kinh tế và các chủ đề tài chính.",
                ImageUrl = "https://example.com/images/kinh-te.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Sách - Truyện thiếu nhi",
                Description = "Sách và truyện dành cho trẻ em.",
                ImageUrl = "https://example.com/images/sach-thieu-nhi.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Sách Teen",
                Description = "Sách dành cho đối tượng tuổi teen.",
                ImageUrl = "https://example.com/images/sach-teen.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Sách cho cha mẹ",
                Description = "Sách cung cấp kiến thức và kinh nghiệm nuôi dạy con cái.",
                ImageUrl = "https://example.com/images/sach-cho-cha-me.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Kỹ năng sống",
                Description = "Sách cung cấp các kỹ năng cần thiết cho cuộc sống.",
                ImageUrl = "https://example.com/images/ky-nang-song.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Thường thức - Đời sống",
                Description = "Sách về các kiến thức phổ thông và đời sống.",
                ImageUrl = "https://example.com/images/thuong-thuc-doi-song.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Y học – Sức khỏe",
                Description = "Sách về các kiến thức y học và chăm sóc sức khỏe.",
                ImageUrl = "https://example.com/images/y-hoc-suc-khoe.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Khoa học – Công nghệ",
                Description = "Sách về các chủ đề khoa học và công nghệ.",
                ImageUrl = "https://example.com/images/khoa-hoc-cong-nghe.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Bách khoa tri thức",
                Description = "Sách cung cấp kiến thức toàn diện về nhiều lĩnh vực.",
                ImageUrl = "https://example.com/images/bach-khoa-tri-thuc.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Danh nhân - Văn hóa - Du lịch",
                Description = "Sách về các danh nhân, văn hóa và du lịch.",
                ImageUrl = "https://example.com/images/danh-nhan-van-hoa-du-lich.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Nghệ thuật - Điện ảnh - Âm nhạc - Nhiếp ảnh",
                Description = "Sách về các lĩnh vực nghệ thuật, điện ảnh, âm nhạc và nhiếp ảnh.",
                ImageUrl = "https://example.com/images/nghe-thuat-dien-anh-am-nhac-nhiep-anh.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Chính trị - Triết học - Tôn giáo - Thiền",
                Description = "Sách về các chủ đề chính trị, triết học, tôn giáo và thiền.",
                ImageUrl = "https://example.com/images/chinh-tri-triet-hoc-ton-giao-thien.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Tử vi - Chiêm tinh",
                Description = "Sách về các chủ đề tử vi và chiêm tinh.",
                ImageUrl = "https://example.com/images/tu-vi-chiem-tinh.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Kiến trúc - Mỹ thuật",
                Description = "Sách về kiến trúc và mỹ thuật.",
                ImageUrl = "https://example.com/images/kien-truc-my-thuat.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Phong thủy",
                Description = "Sách về phong thủy.",
                ImageUrl = "https://example.com/images/phong-thuy.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Sách Chuyên ngành - Học thuật - Khảo cứu",
                Description = "Sách chuyên ngành, học thuật và nghiên cứu.",
                ImageUrl = "https://example.com/images/sach-chuyen-nganh-hoc-thuat-khao-cuu.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Ngoại ngữ",
                Description = "Sách học ngoại ngữ.",
                ImageUrl = "https://example.com/images/ngoai-ngu.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Tin học",
                Description = "Sách về tin học và công nghệ thông tin.",
                ImageUrl = "https://example.com/images/tin-hoc.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Thể dục - Thể thao",
                Description = "Sách về các hoạt động thể dục và thể thao.",
                ImageUrl = "https://example.com/images/the-duc-the-thao.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Từ điển",
                Description = "Các loại từ điển.",
                ImageUrl = "https://example.com/images/tu-dien.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Sách giáo khoa - Sách tham khảo",
                Description = "Sách giáo khoa và sách tham khảo cho học sinh.",
                ImageUrl = "https://example.com/images/sach-giao-khoa-sach-tham-khao.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            },
            new Category
            {
                Id = Guid.NewGuid(),
                FullName = "Sách Ngoại Văn",
                Description = "Sách bằng ngôn ngữ nước ngoài.",
                ImageUrl = "https://example.com/images/sach-ngoai-van.jpg",
                ParentCategoryId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            }
        };
    }

    public static List<OrderStatus> InitOrderStatuses()
    {
        return new List<OrderStatus>
        {
            new OrderStatus { Id = Guid.NewGuid(), FullName = "Chờ xác nhận" },
            new OrderStatus { Id = Guid.NewGuid(), FullName = "Đã xác nhận" },
            new OrderStatus { Id = Guid.NewGuid(), FullName = "Đang vận chuyển" },
            new OrderStatus { Id = Guid.NewGuid(), FullName = "Đã giao hàng" },
            new OrderStatus { Id = Guid.NewGuid(), FullName = "Đã hủy" },
            new OrderStatus { Id = Guid.NewGuid(), FullName = "Hoàn trả" }
        };
    }
}
