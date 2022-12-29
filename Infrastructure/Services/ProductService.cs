using AutoMapper;
using Domain.Wrapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ProductService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetProductDto>>> GetProducts()
    {
            var list = await _context.Products.ToListAsync();
            return new Response<List<GetProductDto>>(_mapper.Map<List<GetProductDto>>(list));
    }
    public async Task<Response<GetProductDto>> GetProductById(int id)
    {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product == null){
                return new Response<GetProductDto>(System.Net.HttpStatusCode.NotFound, "Product not found!");
            }
            return new Response<GetProductDto>(_mapper.Map<GetProductDto>(product));
    }

    public async Task<Response<AddProductDto>> InsertProduct(AddProductDto product)
    {
        try
        {
            var mapped = _mapper.Map<Product>(product);
            await _context.Products.AddAsync(mapped);
            await _context.SaveChangesAsync();
            product.ProductId = mapped.ProductId;
            return new Response<AddProductDto>(product);
        }
        catch (Exception ex)
        {
            return new Response<AddProductDto>(System.Net.HttpStatusCode.InternalServerError, "Internal server error");
        }
    }
    public async Task<Response<AddProductDto>> UpdateProduct(AddProductDto product)
    {
        try
        {
            var find = await _context.Products.FindAsync(product.ProductId);
            find.ProductCategory = product.ProductCategory;
            find.ProductName = product.ProductName;
            find.ProductPrice = product.ProductPrice;
            await _context.SaveChangesAsync();
            return new Response<AddProductDto>(_mapper.Map<AddProductDto>(find));
        }
        catch (Exception ex)
        {
            return new Response<AddProductDto>(System.Net.HttpStatusCode.InternalServerError, "Internal server error");
        }

    }
    public async Task<Response<string>> DeleteProduct(int id)
    {
        var find = await _context.Products.FindAsync(id);
        if (find == null){
            return new Response<string>(System.Net.HttpStatusCode.NotFound, "Product not found!");
        }
        _context.Products.Remove(find);
        var response = await _context.SaveChangesAsync();
        if (response > 0)
            return new Response<string>("Category deleted.");
            return new Response<string>(System.Net.HttpStatusCode.NotFound, "Product not found!");
    }
}