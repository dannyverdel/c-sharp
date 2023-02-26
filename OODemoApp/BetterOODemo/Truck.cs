namespace BetterOODemo;

public class Truck : LandVehicle, IRental
{
    public TruckType Style { get; set; }
    public int RentalId { get; set; }
    public string CurrentRenter { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
}