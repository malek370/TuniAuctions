using System;
using backend.DTOs;
using backend.Helpers;

namespace backend.Services.AccountService;

public interface IAccountService
{
    public Task<CustomResponse<LoginResultDTO>> Login(LoginReqDTO loginUser);
    public Task<CustomResponse<LoginResultDTO>> Register(RegisterDTO registerUser);
}
