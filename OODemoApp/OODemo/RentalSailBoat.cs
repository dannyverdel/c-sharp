namespace OODemo;

public class RentalSailBoat : RentalVehicle
{
    public override void StartEngine() {
        throw new Exception("I do not have an engine to start");
    }
    public override void StopEngine() {
        throw new Exception("I do not have an engine to stop");
    }
}
