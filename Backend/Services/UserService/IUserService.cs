using System;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.UserService;

public interface IUserService
{
    Task<AppUser> GetUserByIdAsync(string id);
    Task<AppUser> GetUserByUsernameAsync(string username);
    Task<AppUser> GetUserToken();
}
