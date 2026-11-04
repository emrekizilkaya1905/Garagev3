using Garagev3.Interfaces;
using Garagev3.Models;

namespace Garagev3.Services;

public class GarageHandler : IHandler
{
    private Garage<IVehicle>? garage;

    public bool HasGarage => garage is not null;
    public int Capacity => garage?.Capacity ?? 0;
    public int Count => garage?.Count ?? 0;

    public void CreateGarage(int capacity)
    {
        garage = new Garage<IVehicle>(capacity);
    }

    public OperationResult Park(IVehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        if (garage is null) return NoGarageResult();


        return garage.Park(vehicle);
    }

    public OperationResult Remove(string registrationNumber)
    {
        return garage?.Remove(registrationNumber) ?? NoGarageResult();
    }

    public IVehicle? FindByRegistrationNumber(string registrationNumber)
    {
        return garage?.FindByRegistrationNumber(registrationNumber);
    }

    public IReadOnlyList<IVehicle> GetAllVehicles()
    {
        return garage is null ? new List<IVehicle>() : garage.GetAllVehicles();
    }

    public IReadOnlyDictionary<string, int> GetVehicleTypeCounts()
    {
        return garage?.GetVehicleTypeCounts() ?? new Dictionary<string, int>();
    }

    public IReadOnlyList<IVehicle> Search(VehicleSearchCriteria criteria)
    {
        ArgumentNullException.ThrowIfNull(criteria);
        return garage is null ? new List<IVehicle>() : garage.Search(criteria);
    }

    public IReadOnlyList<OperationResult> AddSampleVehicles()
    {
        if (garage is null) return new List<OperationResult> { NoGarageResult() };

        List<Vehicle> samples =
        [
            new Motorcycle("MCA001", "Pink", 3, 600),
            new Airplane("AIR001", "White", 3, 2),
            new Car("ABC123", "Black", 4, FuelType.Gasoline),
            new Bus("BUS001", "Red", 6, 40),
            new Boat("BOA001", "Blue", 0, 8.5)
        ];
        var results = new List<OperationResult>();
        foreach (Vehicle vehicle in samples)
        {
            OperationResult result = Park(vehicle);
            results.Add(new OperationResult(result.Success, $"{vehicle.RegistrationNumber}: {result.Message}"));
        }
        return results;
    }

    private static OperationResult NoGarageResult()
    {
        return new OperationResult(false, "Create a garage first.");
    }
}

