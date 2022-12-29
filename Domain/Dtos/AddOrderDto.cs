namespace Domain.Dtos;
using System.ComponentModel.DataAnnotations;
public class AddOrderDto{
    public int OrderId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int InstallementId { get; set; }
}