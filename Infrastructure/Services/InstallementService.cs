using AutoMapper;
using Domain.Wrapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services;

public class InstallementService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public InstallementService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetInstallementDto>>> GetInstallements()
    {
        var list = await _context.Installements.ToListAsync();
        var response = _mapper.Map<List<GetInstallementDto>>(list);
        return new Response<List<GetInstallementDto>>(response);
    }
    public async Task<Response<GetInstallementFullInfoDto>> GetInstallementsInfoCustomer(string productName, double productPrice, string phoneNumber, InstallementPeriod installementPeriod)
    {
        try
        {
            //Installement must be added in advance via InsertInstallement.
            var installement = await (from ins in _context.Installements
                                      join pr in _context.Products on ins.ProductId equals pr.ProductId
                                      where pr.ProductName == productName && pr.ProductPrice == productPrice && ins.InstallementPeriod == installementPeriod
                                      select new GetInstallementFullInfoDto()
                                      {
                                          ProductId = pr.ProductId,
                                          ProductCategory = pr.ProductCategory,
                                          ProductName = pr.ProductName,
                                          InstallementPeriod = ins.InstallementPeriod,
                                          InstallementAmount = ins.InstallementAmount,
                                      }).FirstOrDefaultAsync();
            installement.PhoneNumber = phoneNumber;
            installement.InstallementAmountPerMonth = Math.Round(installement.InstallementAmount / (int)installementPeriod, 2);
            return new Response<GetInstallementFullInfoDto>(installement);
        }
        //Unadded Installements can not be seen. Object reference null.
        catch (Exception ex)
        {
            return new Response<GetInstallementFullInfoDto>(HttpStatusCode.InternalServerError,"Product installement is not registered.");
        }
    }
    public async Task<Response<GetInstallementDto>> GetInstallementById(int id)
    {
        var installement = await _context.Installements.FirstOrDefaultAsync(x => x.InstallementId == id);
        if (installement == null)
        {
            return new Response<GetInstallementDto>(System.Net.HttpStatusCode.NotFound, "Installement not found!");
        }
        return new Response<GetInstallementDto>(_mapper.Map<GetInstallementDto>(installement));
    }

    public async Task<Response<AddInstallementDto>> InsertInstallement(AddInstallementDto installement)
    {
            if ((int)installement.InstallementPeriod == 3 || (int)installement.InstallementPeriod == 6 || (int)installement.InstallementPeriod == 9 || (int)installement.InstallementPeriod == 12 || (int)installement.InstallementPeriod == 18 || (int)installement.InstallementPeriod == 24)
            {var amount = await (from pr in _context.Products
                                    // from ins in _context.Installements
                                    // join pr in _context.Products on ins.ProductId equals pr.ProductId
                                where pr.ProductId == installement.ProductId
                                select new
                                {
                                    ProductCategory = pr.ProductCategory,
                                    ProductPrice = pr.ProductPrice,
                                }).FirstOrDefaultAsync();
            installement.InstallementAmount = InstallementAmount(amount.ProductCategory, amount.ProductPrice, installement.InstallementPeriod);
            var mapped = _mapper.Map<Installement>(installement);
            await _context.Installements.AddAsync(mapped);
            await _context.SaveChangesAsync();
            installement.InstallementId = mapped.InstallementId;
           
            // If the period is any other than 3,6,9,12,18 or 24.
            return new Response<AddInstallementDto>(installement);}
            else {
                return new Response<AddInstallementDto>(System.Net.HttpStatusCode.InternalServerError, "Internal server error");
            }
    }
    public async Task<Response<AddInstallementDto>> UpdateInstallement(AddInstallementDto installement)
    {
        try
        {
            var find = await _context.Installements.FindAsync(installement.InstallementId);
            find.InstallementPeriod = installement.InstallementPeriod;
            find.InstallementInterest = installement.InstallementInterest;
            find.ProductId = installement.ProductId;
            //for updating amount
            var product = await (from pr in _context.Products
                                      where pr.ProductId == installement.ProductId
                                      select new AddProductDto()
                                      {
                                          ProductId = pr.ProductId,
                                          ProductPrice = pr.ProductPrice,
                                          ProductCategory = pr.ProductCategory,
                                          ProductName = pr.ProductName
                                      }).FirstOrDefaultAsync();
            find.InstallementAmount = InstallementAmount(product.ProductCategory,product.ProductPrice,installement.InstallementPeriod);
            await _context.SaveChangesAsync();
            return new Response<AddInstallementDto>(_mapper.Map<AddInstallementDto>(find));
        }
        catch (Exception ex)
        {
            return new Response<AddInstallementDto>(System.Net.HttpStatusCode.InternalServerError, "Internal server error");
        }

    }
    public async Task<Response<string>> DeleteInstallement(int id)
    {
        var find = await _context.Installements.FindAsync(id);
        if (find == null)
        {
            return new Response<string>(System.Net.HttpStatusCode.NotFound, "Installement not found!");
        }
        _context.Installements.Remove(find);
        var response = await _context.SaveChangesAsync();
        if (response > 0)
            return new Response<string>("Installement deleted.");
        return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Installement not found.");
    }
    //Method for counting the interestAmount.
    public double InstallementAmount(ProductCategory productCategory, double productPrice, InstallementPeriod installementPeriod)
    {
        if ((int)productCategory == 0)  //Cellphone
        {
            if ((int)installementPeriod == 3 || (int)installementPeriod == 6 || (int)installementPeriod == 9)
            { return productPrice; }
            else if ((int)installementPeriod == 12)
            { return ((100 + ((int)InstallementInterest.Cellphone)) * productPrice) / 100; }
            else if ((int)installementPeriod == 18)
            { return ((100 + 2 * ((int)InstallementInterest.Cellphone)) * productPrice) / 100; }
            else //if ((int)installementPeriod == 24)
            { return ((100 + 3 * ((int)InstallementInterest.Cellphone)) * productPrice) / 100; }
            // else { return 0; }
        }
        else if ((int)productCategory == 1)  //Computer
        {
            if (((int)installementPeriod == 3 || (int)installementPeriod == 6 || (int)installementPeriod == 9 || (int)installementPeriod == 12))
            { return productPrice; }
            else if ((int)installementPeriod == 18)
            { return ((100 + ((int)InstallementInterest.Computer)) * productPrice) / 100; }
            else //if ((int)installementPeriod == 24)
            { return ((100 + 2 * ((int)InstallementInterest.Computer)) * productPrice) / 100; }
           // else { return 0; }
        }
        else  //Television
        {
            if ((int)installementPeriod == 3 || (int)installementPeriod == 6 || (int)installementPeriod == 9 || (int)installementPeriod == 12 || (int)installementPeriod == 18)
            { return productPrice; }
            else //if ((int)installementPeriod == 24)
            { return ((100 + ((int)InstallementInterest.Television)) * productPrice) / 100; }
           // else { return 0; }
        }
    }
}