using AutoMapper;
using Domain.Wrapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public OrderService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetOrderDto>>> GetOrders()
    {
        var list = await _context.Orders.ToListAsync();
        var response = _mapper.Map<List<GetOrderDto>>(list);
        return new Response<List<GetOrderDto>>(response);
    }
    public async Task<Response<GetOrderDto>> GetOrderById(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        if (order == null)
        {
            return new Response<GetOrderDto>(System.Net.HttpStatusCode.NotFound, "Order not found.");
        }
        return new Response<GetOrderDto>(_mapper.Map<GetOrderDto>(order));
    }

    public async Task<Response<AddOrderDto>> InsertOrder(AddOrderDto order)
    {
        try
        {
            var mapped = _mapper.Map<Order>(order);
            await _context.Orders.AddAsync(mapped);
            await _context.SaveChangesAsync();
            order.OrderId = mapped.OrderId;
            return new Response<AddOrderDto>(order);
        }
        catch
        {
            return new Response<AddOrderDto>(System.Net.HttpStatusCode.InternalServerError, "Internal server error");
        }
    }
    public async Task<Response<AddOrderDto>> UpdateOrder(AddOrderDto order)
    {
        try
        {
            var find = await _context.Orders.FindAsync(order.OrderId);
            find.CustomerId = order.CustomerId;
            find.InstallementId = order.InstallementId;
            await _context.SaveChangesAsync();
            return new Response<AddOrderDto>(_mapper.Map<AddOrderDto>(find));
        }
        catch
        {
            return new Response<AddOrderDto>(System.Net.HttpStatusCode.InternalServerError, "Internal Server error.");
        }
    }
    public async Task<Response<string>> DeleteOrder(int id)
    {
        var find = await _context.Orders.FindAsync(id);
        _context.Orders.Remove(find);
        var response = await _context.SaveChangesAsync();
        if (response > 0)
            return new Response<string>("Order deleted.");
            return new Response<string>(System.Net.HttpStatusCode.NotFound, "Order not found.");
    }
}