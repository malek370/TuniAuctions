using AutoMapper;
using backend.DTOs;
using backend.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace backend.Services.AccountService;

public class AccountService(UserManager<IdentityUser> _userManager,IMapper _mapper) : IAccountService
{
    public async Task<CustomResponse<LoginResultDTO>>  Login(LoginReqDTO loginUser)
    {
        var user =await  _userManager.Users.Where(u=>u.Email==loginUser.Email).FirstOrDefaultAsync();
        if(user == null)return new CustomResponse<LoginResultDTO>("User not found",404);
        var checkPassword= await _userManager.CheckPasswordAsync(user,loginUser.Password);
        if(!checkPassword)return new CustomResponse<LoginResultDTO>("Wrong password",404);
        return GenerateResWithToken(user);

    }

    public async Task<CustomResponse<LoginResultDTO>> Register(RegisterDTO registerUserDTO)
    {
        if(UserExists(registerUserDTO))return new CustomResponse<LoginResultDTO>("User username or Email already exists",409);
        IdentityUser user= _mapper.Map<IdentityUser>(registerUserDTO);
        await _userManager.CreateAsync(user);
        await _userManager.AddPasswordAsync(user,registerUserDTO.Password);
        await _userManager.AddToRoleAsync(user,"Member");
        return GenerateResWithToken(user);

    }
    private bool UserExists(RegisterDTO register)
    =>_userManager.Users.Any(u=>u.UserName==register.UserName||u.Email==register.Email);
    private static CustomResponse<LoginResultDTO> GenerateResWithToken(IdentityUser user)
    {
        var GenToken= "Token to generate";
        return new CustomResponse<LoginResultDTO>(
            new LoginResultDTO{username=user.UserName!,Token=GenToken},
            "OK",
            200
        );
    }
}
