namespace Domain.Dtos;
using Domain.Entities;
public class GetInstallementFullInfoDto
{
    public int ProductId { get; set; }

    public ProductCategory ProductCategory { get; set; }
    public string ProductName { get; set; }
    public double InstallementAmount { get; set; }
    public double InstallementAmountPerMonth { get; set; }
    public InstallementPeriod InstallementPeriod {get; set;}
    public string PhoneNumber { get; set; }
}