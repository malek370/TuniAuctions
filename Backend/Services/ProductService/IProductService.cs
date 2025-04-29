using System;
using backend.DTOs;
using backend.Helpers;

namespace backend.Services.ProductService;

public interface IProductService
{
    Task<CustomResponse<object>> CreateProduct(ProductDTO creatProd);
    Task<CustomResponse<ProductDTO>> GetById(string prodId);
    Task<CustomResponse<List<ProductDTO>>> GetByUser(string username);
    Task<CustomResponse<List<ProductDTO>>> ProductsGroup();
    Task<CustomResponse<object>> DeleteProduct(string ProdId);
}
