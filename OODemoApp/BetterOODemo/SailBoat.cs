namespace BetterOODemo;

public class SailBoat : IRental
{
    public int RentalId { get; set; }
    public string CurrentRenter { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
}