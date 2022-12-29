using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class CustomerController{
    private readonly CustomerService _customerService;
    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }
    [HttpGet("GetCustomers")]
    public async Task<Response<List<GetCustomerDto>>> GetCustomers() {
        return await _customerService.GetCustomers();
    }
    [HttpGet("GetCustomerById")]
    public async Task<Response<GetCustomerDto>> GetCustomerById(int id){
        return await _customerService.GetCustomerById(id);
    }
    [HttpPost("InsertCustomer")]
    public async Task<Response<AddCustomerDto>> InsertCustomer(AddCustomerDto customer){
        return await _customerService.InsertCustomer(customer);
    }
    [HttpPut("UpdateCustomer")]
    public async Task<Response<AddCustomerDto>> UpdateCustomer(AddCustomerDto customer){
        return await _customerService.UpdateCustomer(customer);
    }
    [HttpDelete("DeleteCustomer")]
    public async Task<Response<string>> DeleteCustomer(int id){
        return await _customerService.DeleteCustomer(id);
    }
}