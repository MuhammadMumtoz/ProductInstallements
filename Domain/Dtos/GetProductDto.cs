namespace Domain.Dtos;
using Domain.Entities;
public class GetProductDto{
    public int ProductId { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
}