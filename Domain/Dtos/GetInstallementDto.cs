using Domain.Entities;

namespace Domain.Dtos;
public class GetInstallementDto
{
    public int InstallementId { get; set; }
    public InstallementPeriod InstallementPeriod { get; set; }
    public InstallementInterest InstallementInterest { get; set; }
    public double InstallementAmount {get; set;}
    public int ProductId { get; set; }
}