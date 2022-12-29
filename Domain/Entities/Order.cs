namespace Domain.Entities;
public class Order{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer {get; set;}
    public int InstallementId { get; set; }
    public Installement Installement { get; set; }
}