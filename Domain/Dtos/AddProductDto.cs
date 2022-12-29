namespace Domain.Dtos;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
public class AddProductDto{
    public int ProductId { get; set; }
    [Range(0,2)]
    public ProductCategory ProductCategory { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public double ProductPrice { get; set; }
}