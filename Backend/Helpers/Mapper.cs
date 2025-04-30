using System;
using AutoMapper;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Helpers;

public class Mapper:Profile
{
    public Mapper()
    {
        CreateMap<RegisterDTO,AppUser>();
        CreateMap<CreateProductDTO,Product>();
        CreateMap<Product,GetProductDTO>();
    }
}
