using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;
public class AddCustomerDto{
    public int CustomerId { get; set; }
    [Required]
    [MinLength(3)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(3)]
    public string LastName { get; set; }
    [Required]
    [MinLength(7)]
    public string PhoneNumber { get; set; }
}