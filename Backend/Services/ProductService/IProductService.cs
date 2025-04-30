using System;
using backend.DTOs;
using backend.Helpers;

namespace backend.Services.ProductService;

public interface IProductService
{
    Task<CustomResponse<object>> CreateProduct(CreateProductDTO creatProd);
    Task<CustomResponse<GetProductDTO>> GetById(string prodId);
    Task<CustomResponse<List<GetProductDTO>>> GetByUser(string username);
    Task<CustomResponse<List<GetProductDTO>>> ProductsGroup();
    Task<CustomResponse<object>> DeleteProduct(string ProdId);
}
