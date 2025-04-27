using System;
using System.Text;
using backend.DataContext;
using backend.Models;
using backend.Services.AccountService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceColleection,ConfigurationManager config)
    {
        // Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
serviceColleection.AddOpenApi();
serviceColleection.AddHttpContextAccessor();
//serviceColleection.AddControllers();
serviceColleection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
serviceColleection.AddScoped<IAccountService,AccountService>();
//add database
serviceColleection.AddDbContext<AppDataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
serviceColleection.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<AppDataContext>();

    serviceColleection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Token key not Found");
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        //to make authorization for SignalR 
        option.Events=new JwtBearerEvents{
            OnMessageReceived = context =>{
                var accesToken=context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if(!string.IsNullOrEmpty(accesToken) && path.StartsWithSegments("/hubs"))
                {
                    context.Token=accesToken;
                }
                return Task.CompletedTask;
            }
        };
    });
        serviceColleection.AddAuthorizationBuilder()
                .AddPolicy(Policies.MemberRole, policy => policy.RequireRole(Roles.Member))
                .AddPolicy(Policies.RequireBuyerRole, policy => policy.RequireRole(Roles.Buyer))
                .AddPolicy(Policies.RequireSellerRole, policy => policy.RequireRole(Roles.Seller));
    return serviceColleection;
    }
    
}
