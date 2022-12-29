using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;
public class GetCustomerDto{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}