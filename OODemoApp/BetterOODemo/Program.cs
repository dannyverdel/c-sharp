namespace BetterOODemo;

class Program
{
    static void Main(string[] args) {
        List<IRental> rentals = new List<IRental>();
        rentals.Add(new Truck() { CurrentRenter = "Danny" });
        rentals.Add(new SailBoat() { CurrentRenter = "Danny" });
        rentals.Add(new Car() { CurrentRenter = "Danny" });

        foreach ( IRental rental in rentals ) {
            if ( rental is Truck t )
                t.StartEngine();
            if ( rental is Car c )
                c.StartEngine();
        }
    }
}
