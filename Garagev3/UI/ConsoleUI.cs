using Garagev3.Interfaces;
using static Garagev3.UI.ConsoleInput;
using static Garagev3.UI.GarageMenuActions;

namespace Garagev3.UI;

public class ConsoleUI : IUI
{
    private readonly IHandler handler;
    private readonly GarageMenuActions actions;

    public ConsoleUI(IHandler handler, GarageMenuActions actions)
    {
        ArgumentNullException.ThrowIfNull(handler);
        ArgumentNullException.ThrowIfNull(actions);
        this.handler = handler;
        this.actions = actions;
    }

    public void Run()
    {
        try
        {
            Console.WriteLine("=== Garage 3.0 ===");
            Console.WriteLine("Create your garage to get started. Use /cancel to exit.");
            handler.CreateGarage(ReadInteger("Capacity", 1));

            Console.WriteLine($"Garage created with {handler.Capacity} spaces.");

            while (true)
            {
                Console.WriteLine("\n=== Garage menu ===");
                Console.WriteLine($"Spaces used: {handler.Count}/{handler.Capacity}");
                Console.WriteLine("1. List vehicles\n2. List vehicle type counts\n3. Park vehicle\n4. Remove vehicle\n5. Find by registration number\n6. Search vehicles\n7. Add sample vehicles\n0. Exit");
                Console.Write("Select an option: ");
                string choice = ReadLine();
                if (choice == "0") break;

                try
                {
                    switch (choice)
                    {
                        case "1": DisplayVehicles(handler.GetAllVehicles()); break;
                        case "2": actions.DisplayTypeCounts(); break;
                        case "3": actions.ParkVehicle(); break;
                        case "4": actions.RemoveVehicle(); break;
                        case "5":
                            IVehicle? vehicle = handler.FindByRegistrationNumber(ReadText("Registration number"));
                            Console.WriteLine(vehicle?.ToString() ?? "No matching vehicle found.");
                            break;
                        case "6": actions.SearchVehicles(); break;
                        case "7": actions.AddSampleVehicles(); break;
                        default: Console.WriteLine("Invalid option. Please try again."); break;
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operation cancelled.");
                }
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Garage creation cancelled.");
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("\nInput closed.");
        }

        Console.WriteLine("Goodbye!");
    }
}






