using Garagev3.Models;
using Garagev3.Interfaces;
using static Garagev3.UI.ConsoleInput;

namespace Garagev3.UI;

public class GarageMenuActions
{
    private readonly IHandler handler;

    public GarageMenuActions(IHandler handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        this.handler = handler;
    }

    public void ParkVehicle()
    {
        if (handler.Count == handler.Capacity)
        {
            Console.WriteLine("The garage is full.");
            return;
        }

        Console.WriteLine(handler.Park(VehicleForm.ReadVehicle()).Message);
    }

    public void RemoveVehicle()
    {
        IReadOnlyList<IVehicle> vehicles = handler.GetAllVehicles();
        if (vehicles.Count == 0)
        {
            Console.WriteLine("No vehicles parked.");
            return;
        }

        Console.WriteLine("\n=== Remove vehicle ===");
        for (int i = 0; i < vehicles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {vehicles[i]}");
        }
        Console.WriteLine("0. Back");

        while (true)
        {
            int selection = ReadInteger("Select a vehicle number", 0);
            if (selection == 0) return;
            if (selection > vehicles.Count)
            {
                Console.WriteLine($"Select a vehicle from 1 to {vehicles.Count}, or 0 to go back.");
                continue;
            }

            IVehicle vehicle = vehicles[selection - 1];
            OperationResult result = handler.Remove(vehicle.RegistrationNumber);
            Console.WriteLine($"{vehicle.RegistrationNumber}: {result.Message}");
            return;
        }
    }

    public void SearchVehicles()
    {
        VehicleSearchCriteria? criteria = VehicleSearchForm.ReadCriteria();
        if (criteria is not null) DisplayVehicles(handler.Search(criteria));
    }

    public void DisplayTypeCounts()
    {
        var counts = handler.GetVehicleTypeCounts();
        if (counts.Count == 0) Console.WriteLine("No vehicles parked.");
        foreach (var entry in counts) Console.WriteLine($"{entry.Key}: {entry.Value}");
    }

    public static void DisplayVehicles(IReadOnlyList<IVehicle> vehicles)
    {
        if (vehicles.Count == 0) Console.WriteLine("No matching vehicles found.");
        foreach (IVehicle vehicle in vehicles) Console.WriteLine(vehicle);
    }

    public void AddSampleVehicles()
    {
        foreach (OperationResult result in handler.AddSampleVehicles())
        {
            Console.WriteLine(result.Message);
        }
    }
}
