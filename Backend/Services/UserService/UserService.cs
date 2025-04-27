using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.UserService;

public class UserService(UserManager<IdentityUser> UserManager,HttpContextAccessor Context) : IUserService
{
    public async Task<IdentityUser> GetUserByIdAsync(string id)
    {
        
        var user= await UserManager.FindByIdAsync(id);
        if(user==null) throw new Exception("User not found");
        return user;

    }
    public async Task<IdentityUser> GetUserToken()
    {
        var userId=Context.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await GetUserByIdAsync(userId!);
    }

    public async Task<IdentityUser> GetUserByUsernameAsync(string username)
    {
        var user= await UserManager.FindByNameAsync(username);
        if(user==null) throw new Exception("User not found");
        return user;
    }
}
