namespace Domain.Entities;
public class Product{
    public int ProductId { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
    public List<Installement> Installements { get; set; }
}
public enum ProductCategory{
    Cellphone,
    Computer,
    Television
}