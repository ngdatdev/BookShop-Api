using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Application.DTOs.Response;

public class LoginResponse
{
    public string AccessToken { get; init; }

    public string RefreshToken { get; init; }

    public UserCredential User { get; init; }

    public class UserCredential
    {
        public string FullName { get; init; }
        public string AvatarUrl { get; set; }
    }
}
