using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Users.GetProfileUser;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class GetProfileUserResponse : IFeatureResponse
{
    public GetProfileUserResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public UserDetail User { get; init; }

        public sealed class UserDetail
        {
            public string Email { get; init; }

            public string Username { get; init; }

            public string NumberPhone { get; init; }

            public string AvatarUrl { get; init; }

            public string FirstName { get; init; }

            public string LastName { get; init; }

            public string Gender { get; init; }

            public AddressUser Address { get; init; }

            public sealed class AddressUser
            {
                public string Ward { get; init; }

                public string District { get; init; }

                public string Province { get; init; }
            }
        }
    }
}
