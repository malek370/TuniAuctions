using System;
using backend.DTOs;
using backend.Models;
using backend.Services.AccountService;
using backend.Services.ProductService;
using backend.Services.UserService;

namespace backend.Extensions;
//NOT USED 
public static class ApisExtension
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
    public static WebApplication AddMinimalApis(this WebApplication app)
    {

        
        app.MapPost("/login", async (LoginReqDTO login, IAccountService accountService) =>
            {
                var res = await accountService.Login(login);
                return res.HandleResponse();
            })
        .WithName("login");
        app.MapPost("/register", async (RegisterDTO registerDTO, IAccountService accountService) =>
            {
                var res = await accountService.Register(registerDTO);
                return res.HandleResponse();
            })
        .WithName("register");
        app.MapPost("/addProduct",async (CreateProductDTO prod,IProductService prodService) =>
        {
            var res=await prodService.CreateProduct(prod);
            return res.HandleResponse();
        }).WithName("creatProduct").RequireAuthorization(Policies.MemberRole);
        app.MapGet("/getProduct/{id}",async(string id,IProductService prodService)=>{
           var res=await prodService.GetById(id);
            return res.HandleResponse(); 
        }).WithName("GetProductId").RequireAuthorization(Policies.MemberRole);
        app.MapDelete("/product/{id}",async (string id,IProductService prodService)=>{
           var res=await prodService.DeleteProduct(id);
            return res.HandleResponse(); 
        }).WithName("deleteProduct").RequireAuthorization(Policies.MemberRole);
        app.MapGet("/getProducts",async(string id,IProductService prodService,IUserService userService)=>{
            var user=await userService.GetUserToken();
           var res=await prodService.GetByUser(user.UserName!);
            return res.HandleResponse(); 
        }).WithName("GetProducts").RequireAuthorization(Policies.MemberRole);
        return app;
    }
}
