namespace Domain.Entities;
public class Installement{
    public int InstallementId { get; set; }
    public InstallementPeriod InstallementPeriod { get; set; }
    public InstallementInterest InstallementInterest { get; set; }
    public double InstallementAmount {get; set;}
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public List<Order> Orders { get; set; }
}
public enum InstallementPeriod{
    Three = 3,
    Six = 6,
    Nine = 9,
    Twelve = 12,
    Eighteen = 18,
    TwentyFour = 24
}

public enum InstallementInterest{
    Cellphone = 3,
    Computer = 4,
    Television = 5
}
