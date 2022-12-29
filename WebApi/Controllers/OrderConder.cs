using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class OrderController{
    private readonly OrderService _orderService;
    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }
    [HttpGet("GetOrders")]
    public async Task<Response<List<GetOrderDto>>> GetOrders() {
        return await _orderService.GetOrders();
    }
    [HttpGet("GetOrderById")]
    public async Task<Response<GetOrderDto>> GetOrderById(int id){
        return await _orderService.GetOrderById(id);
    }
    [HttpPost("InsertOrder")]
    public async Task<Response<AddOrderDto>> InsertOrder(AddOrderDto order){
        return await _orderService.InsertOrder(order);
    }
    [HttpPut("UpdateOrder")]
    public async Task<Response<AddOrderDto>> UpdateOrder(AddOrderDto order){
        return await _orderService.UpdateOrder(order);
    }
    [HttpDelete("DeleteOrder")]
    public async Task<Response<string>> DeleteOrder(int id){
        return await _orderService.DeleteOrder(id);
    }
}