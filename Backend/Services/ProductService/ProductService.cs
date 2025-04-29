using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.DataContext;
using backend.DTOs;
using backend.Helpers;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.ProductService;

public class ProductService(IMapper Mapper,AppDataContext DataContext) : IProductService
{
    public async Task<CustomResponse<object>> CreateProduct(ProductDTO creatProd)
    {
        var newProd=Mapper.Map<Product>(creatProd);
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

    public async Task<CustomResponse<ProductDTO>> GetById(string prodId)
    {
        var prod=await DataContext.Products.FirstOrDefaultAsync(p=>p.Id==prodId);
        if (prod==null)return new CustomResponse<ProductDTO>("Product not found",404);
        return new CustomResponse<ProductDTO>(Mapper.Map<ProductDTO>(prod),"Product found",200);
    }

    public async Task<CustomResponse<List<ProductDTO>>> GetByUser(string username)
    {
        var prods=await DataContext.Products.Where(p=>p.Owner.UserName==username).Select(p=>Mapper.Map<ProductDTO>(p)).ToListAsync();
         return new CustomResponse<List<ProductDTO>>(prods,"Products of"+username,200);
    }

    public async Task<CustomResponse<List<ProductDTO>>> ProductsGroup()
    {
        var prods=await DataContext.Products.Take(10).OrderBy(p=>EF.Functions.Random()).Select(p=>Mapper.Map<ProductDTO>(p)).ToListAsync();
         return new CustomResponse<List<ProductDTO>>(prods,"Products for auction",200);
    }
}
