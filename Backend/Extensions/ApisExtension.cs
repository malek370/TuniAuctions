using System;
using backend.DTOs;
using backend.Services.AccountService;

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

        var summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast");
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
        return app;
    }
}
