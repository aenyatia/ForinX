namespace ForinX;

public class Aircraft : Entity
{
    private readonly AircraftType _aircraftType;
    
    public Aircraft(AircraftType aircraftType)
    {
        _aircraftType = aircraftType;
    }

    public enum AircraftType
    {
        Eagle,
        Raptor
    }
}