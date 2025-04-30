using System;
using System.Security.Claims;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.UserService;

public class UserService(UserManager<AppUser> UserManager,IHttpContextAccessor Context) : IUserService
{
    public async Task<AppUser> GetUserByIdAsync(string id)
    {
        
        var user= await UserManager.FindByIdAsync(id);
        if(user==null) throw new Exception("User not found");
        return user;

    }
    public async Task<AppUser> GetUserToken()
    {
        var userId=Context.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await GetUserByIdAsync(userId!);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        var user= await UserManager.FindByNameAsync(username);
        if(user==null) throw new Exception("User not found");
        return user;
    }
}
