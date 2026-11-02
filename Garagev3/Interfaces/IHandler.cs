using Garagev3.Models;

namespace Garagev3.Interfaces;

public interface IHandler
{
    bool HasGarage { get; }
    int Capacity { get; }
    int Count { get; }

    void CreateGarage(int capacity);
    OperationResult Park(IVehicle vehicle);
    OperationResult Remove(string registrationNumber);
    IVehicle? FindByRegistrationNumber(string registrationNumber);
    IReadOnlyList<IVehicle> GetAllVehicles();
    IReadOnlyDictionary<string, int> GetVehicleTypeCounts();
    IReadOnlyList<IVehicle> Search(VehicleSearchCriteria criteria);
    IReadOnlyList<OperationResult> AddSampleVehicles();
}
