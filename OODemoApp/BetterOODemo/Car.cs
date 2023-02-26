namespace BetterOODemo;

public class Car : LandVehicle, IRental
{
    public int RentalId { get; set; }
    public string CurrentRenter { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public CarType Style { get; set; }
}