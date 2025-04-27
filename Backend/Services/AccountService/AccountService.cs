using System.Security.Claims;
using System.Text;
using AutoMapper;
using backend.DTOs;
using backend.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
namespace backend.Services.AccountService;

public class AccountService(UserManager<IdentityUser> _userManager,IMapper _mapper,IConfiguration config) : IAccountService
{
    public async Task<CustomResponse<LoginResultDTO>>  Login(LoginReqDTO loginUser)
    {
        var user =await  _userManager.Users.Where(u=>u.Email==loginUser.Email).FirstOrDefaultAsync();
        if(user == null)return new CustomResponse<LoginResultDTO>("User not found",404);
        var checkPassword= await _userManager.CheckPasswordAsync(user,loginUser.Password);
        if(!checkPassword)return new CustomResponse<LoginResultDTO>("Wrong password",404);
        return await GenerateResWithToken(user);

    }

    public async Task<CustomResponse<LoginResultDTO>> Register(RegisterDTO registerUserDTO)
    {

        if(UserExists(registerUserDTO))return new CustomResponse<LoginResultDTO>("Username or Email already exists",409);
        if(registerUserDTO.ConfirmPassword!=registerUserDTO.Password)return new CustomResponse<LoginResultDTO>("Passwords do not match",400);
        IdentityUser user= _mapper.Map<IdentityUser>(registerUserDTO);
        var result=await _userManager.CreateAsync(user,registerUserDTO.Password);
        if(result.Succeeded)return await GenerateResWithToken(user);
        return new CustomResponse<LoginResultDTO>(
            
            string.Join(", ", result.Errors.Select(e => e.Description))??"Unexpected error",
            500
        );

    }
    private bool UserExists(RegisterDTO register)
    =>_userManager.Users.Any(u=>u.NormalizedUserName==register.UserName.ToUpper()||u.Email==register.Email);
    private async Task<CustomResponse<LoginResultDTO>> GenerateResWithToken(IdentityUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        if(user.UserName==null) throw new Exception("no username for user");
        var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new(ClaimTypes.Name,user.UserName!)
            };
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };
        var TokenHandler = new JsonWebTokenHandler();
        var GenToken = TokenHandler.CreateToken(tokenDescriptor);
        return new CustomResponse<LoginResultDTO>(
            new LoginResultDTO{Username=user.UserName!,Token=GenToken},
            "OK",
            200
        );
    }
     
}
