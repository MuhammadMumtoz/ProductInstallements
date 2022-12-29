using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class InstallementController{
    private readonly InstallementService _installementService;
    public InstallementController(InstallementService installementService)
    {
        _installementService = installementService;
    }
    [HttpGet("GetInstallements")]
    public async Task<Response<List<GetInstallementDto>>> GetInstallements() {
        return await _installementService.GetInstallements();
    }
    [HttpGet("GetInstallementById")]
    public async Task<Response<GetInstallementDto>> GetInstallementById(int id){
        return await _installementService.GetInstallementById(id);
    }
    [HttpGet("GetInstallementFullInfo")]
    public async Task<Response<GetInstallementFullInfoDto>> GetInstallementsInfoCustomer(string productName, double productPrice, string phoneNumber, InstallementPeriod installementPeriod){
    return await _installementService.GetInstallementsInfoCustomer(productName,productPrice,phoneNumber,installementPeriod);
    }
    [HttpPost("InsertInstallement")]
    public async Task<Response<AddInstallementDto>> InsertInstallement(AddInstallementDto installement){
        return await _installementService.InsertInstallement(installement);
    }
    [HttpPut("UpdateInstallement")]
    public async Task<Response<AddInstallementDto>> UpdateInstallement(AddInstallementDto installement){
        return await _installementService.UpdateInstallement(installement);
    }
    [HttpDelete("DeleteInstallement")]
    public async Task<Response<string>> DeleteInstallement(int id){
        return await _installementService.DeleteInstallement(id);
    }
}