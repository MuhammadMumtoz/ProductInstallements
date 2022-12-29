namespace Infrastructure.Mappers;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

public class ProfileService : Profile {
    public ProfileService(){
        CreateMap<GetCustomerDto, AddCustomerDto>().ReverseMap();
        CreateMap<GetCustomerDto, Customer>().ReverseMap();
        CreateMap<AddCustomerDto, Customer>().ReverseMap();
        CreateMap<GetOrderDto, AddOrderDto>().ReverseMap();
        CreateMap<GetOrderDto, Order>().ReverseMap();
        CreateMap<AddOrderDto, Order>().ReverseMap();
        CreateMap<GetProductDto, AddProductDto>().ReverseMap();
        CreateMap<GetProductDto, Product>().ReverseMap();
        CreateMap<AddProductDto, Product>().ReverseMap();
        CreateMap<GetInstallementDto, AddInstallementDto>().ReverseMap();
        CreateMap<GetInstallementDto, Installement>().ReverseMap();
        CreateMap<AddInstallementDto, Installement>().ReverseMap();
    }
}