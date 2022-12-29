using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Dtos;
public class AddInstallementDto
{
    public int InstallementId { get; set; }
    [Range(3,24)]
    public InstallementPeriod InstallementPeriod { get; set; }
    [Range(3,5)]
    public InstallementInterest InstallementInterest { get; set; }
    public double InstallementAmount {get; set;}
    public int ProductId { get; set; }
}