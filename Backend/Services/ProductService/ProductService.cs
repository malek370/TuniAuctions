using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.DataContext;
using backend.DTOs;
using backend.Helpers;
using backend.Models;
using backend.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.ProductService;

public class ProductService(IMapper Mapper,AppDataContext DataContext,IUserService UserService) : IProductService
{
    public async Task<CustomResponse<object>> CreateProduct(CreateProductDTO creatProd)
    {
        var newProd=Mapper.Map<Product>(creatProd);
        newProd.Owner=await UserService.GetUserToken();
        await DataContext.AddAsync(newProd);
        await DataContext.SaveChangesAsync();
        return new CustomResponse<object>("Product created",200);

    }

    public async Task<CustomResponse<object>> DeleteProduct(string ProdId)
    {
        var prod=await DataContext.Products.FirstOrDefaultAsync(p=>p.Id==ProdId);
        if (prod==null)return new CustomResponse<object>("Product not found",404);
        DataContext.Products.Remove(prod);
        await DataContext.SaveChangesAsync();
        return new CustomResponse<object>("Product deleted",200);
    }

    public async Task<CustomResponse<GetProductDTO>> GetById(string prodId)
    {
        var prod=await DataContext.Products.FirstOrDefaultAsync(p=>p.Id==prodId);
        if (prod==null)return new CustomResponse<GetProductDTO>("Product not found",404);
        return new CustomResponse<GetProductDTO>(Mapper.Map<GetProductDTO>(prod),"Product found",200);
    }

    public async Task<CustomResponse<List<GetProductDTO>>> GetByUser(string username)
    {
        var prods=await DataContext.Products.Where(p=>p.Owner.UserName==username).Select(p=>Mapper.Map<GetProductDTO>(p)).ToListAsync();
         return new CustomResponse<List<GetProductDTO>>(prods,"Products of"+username,200);
    }

    public async Task<CustomResponse<List<GetProductDTO>>> ProductsGroup()
    {
        var prods=await DataContext.Products.Take(10).OrderBy(p=>EF.Functions.Random()).Select(p=>Mapper.Map<GetProductDTO>(p)).ToListAsync();
         return new CustomResponse<List<GetProductDTO>>(prods,"Products for auction",200);
    }
}
