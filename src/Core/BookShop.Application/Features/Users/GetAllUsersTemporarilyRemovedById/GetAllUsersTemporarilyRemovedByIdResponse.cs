using System;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;

namespace BookShop.Application.Features.Users.GetAllUsersTemporarilyRemovedById;

/// <summary>
///     GetAllUsersTemporarilyRemovedById Response
/// </summary>
public class GetAllUsersTemporarilyRemovedByIdResponse : IFeatureResponse
{
    public GetAllUsersTemporarilyRemovedByIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<User> Users { get; init; }

        public sealed class User
        {
            public Guid Id { get; init; }
            public string Username { get; init; }

            public string FullName { get; init; }

            public string Email { get; init; }

            public string AvatarUrl { get; init; }

            public string Gender { get; init; }
        }
    }
}
