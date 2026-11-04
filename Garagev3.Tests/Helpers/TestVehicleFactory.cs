using Garagev3.Models;
using Garagev3.Services;

namespace Garagev3.Tests.Helpers;

internal static class TestVehicleFactory
{
    public static Car Car(string registration = "ABC123", string color = "Black") => new(registration, color, 4, FuelType.Gasoline);

    public static Garage<Vehicle> MixedGarage()
    {
        var garage = new Garage<Vehicle>(5);
        garage.Park(Car());
        garage.Park(Car("DEF456", "Red"));
        garage.Park(new Motorcycle("MCA001", "Pink", 3, 600));
        garage.Park(new Boat("BOA001", "Black", 0, 8));
        return garage;
    }
}

