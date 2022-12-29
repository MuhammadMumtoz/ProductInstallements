using AutoMapper;
using Domain.Wrapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CustomerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CustomerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetCustomerDto>>> GetCustomers()
    {
        var list = await _context.Customers.ToListAsync();
        var response = _mapper.Map<List<GetCustomerDto>>(list);
        return new Response<List<GetCustomerDto>>(response);
    }
    public async Task<Response<GetCustomerDto>> GetCustomerById(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
        if (customer == null)
        {
            return new Response<GetCustomerDto>(System.Net.HttpStatusCode.NotFound, "Customer not found!");
        }
        return new Response<GetCustomerDto>(_mapper.Map<GetCustomerDto>(customer));
    }

    public async Task<Response<AddCustomerDto>> InsertCustomer(AddCustomerDto customer)
    {
        try
        {
            var mapped = _mapper.Map<Customer>(customer);
            await _context.Customers.AddAsync(mapped);
            await _context.SaveChangesAsync();
            customer.CustomerId = mapped.CustomerId;
            return new Response<AddCustomerDto>(customer);
        }
        catch
        {
            return new Response<AddCustomerDto>(System.Net.HttpStatusCode.InternalServerError, "Internal server error.");
        }
    }
    public async Task<Response<AddCustomerDto>> UpdateCustomer(AddCustomerDto customer)
    {
        try
        {
            var find = await _context.Customers.FindAsync(customer.CustomerId);
            find.FirstName = customer.FirstName;
            find.LastName = customer.LastName;
            find.PhoneNumber = customer.PhoneNumber;
            await _context.SaveChangesAsync();
            return new Response<AddCustomerDto>(_mapper.Map<AddCustomerDto>(find));
        }
        catch
        {
            return new Response<AddCustomerDto>(System.Net.HttpStatusCode.InternalServerError, "Internal server error.");
        }
    }
    public async Task<Response<string>> DeleteCustomer(int id)
    {
        var find = await _context.Customers.FindAsync(id);
        if (find == null)
        {
            return new Response<string>(System.Net.HttpStatusCode.NotFound, "Customer not found!");
        }
        _context.Customers.Remove(find);
        var response = await _context.SaveChangesAsync();
        if (response > 0)
            return new Response<string>("Customer deleted.");
        return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Customer not found.");
    }
}