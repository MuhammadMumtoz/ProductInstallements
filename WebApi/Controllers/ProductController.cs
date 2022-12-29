using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class ProductController{
    private readonly ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    [HttpGet("GetProducts")]
    public async Task<Response<List<GetProductDto>>> GetProducts() {
        return await _productService.GetProducts();
    }
    [HttpGet("GetProductById")]
    public async Task<Response<GetProductDto>> GetProductById(int id){
        return await _productService.GetProductById(id);
    }
    [HttpPost("InsertProduct")]
    public async Task<Response<AddProductDto>> InsertProduct(AddProductDto product){
        return await _productService.InsertProduct(product);
    }
    [HttpPut("UpdateProduct")]
    public async Task<Response<AddProductDto>> UpdateProduct(AddProductDto product){
        return await _productService.UpdateProduct(product);
    }
    [HttpDelete("DeleteProduct")]
    public async Task<Response<string>> DeleteProduct(int id){
        return await _productService.DeleteProduct(id);
    }
}