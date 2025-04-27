using System;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.UserService;

public interface IUserService
{
        Task<IdentityUser> GetUserByIdAsync(string id);
    Task<IdentityUser> GetUserByUsernameAsync(string username);
    Task<IdentityUser> GetUserToken();
}
