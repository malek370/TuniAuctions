using System;
using AutoMapper;
using backend.DTOs;
using Microsoft.AspNetCore.Identity;

namespace backend.Helpers;

public class Mapper:Profile
{
    public Mapper()
    {
        CreateMap<RegisterDTO,IdentityUser>();
    }
}
