using TuberTreats.Models;
using TuberTreats.Models.DTOs;

namespace TuberTreats.Menus;

public class TuberDriverMenu
{
    private readonly List<TuberDriver> _drivers;
    private readonly List<TuberOrder> _orders;

    public TuberDriverMenu(List<TuberDriver> drivers, List<TuberOrder> orders)
    {
        {
            _drivers = drivers;
            _orders = orders;
        }
    }

    public void ShowMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== /tuberdrivers Menu ===");
            Console.WriteLine("1. Get all Drivers");
            Console.WriteLine("2. Get a Driver by ID");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Select an action: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllDrivers();
                    break;
                case "2":
                    DisplayDriverById();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void DisplayAllDrivers()
    {
        Console.Clear();
        Console.WriteLine("All Drivers:");
        var driverDTOs = _drivers.Select(MapToDTO).ToList();
        foreach (var driver in driverDTOs)
        {
            Console.WriteLine($"Driver ID: {driver.Id}, Name: {driver.Name}");
        }
        WaitForInput();
    }

    private void DisplayDriverById()
    {
        Console.Clear();
        Console.Write("Enter Driver ID: ");
        if (int.TryParse(Console.ReadLine(), out int driverId))
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == driverId);
            if (driver != null)
            {
                var driverDTO = MapToDTO(driver);
                Console.WriteLine($"Driver ID: {driverDTO.Id}, Name: {driverDTO.Name}");
                Console.WriteLine("Deliveries:");
                foreach (var deliveryId in driverDTO.DeliveryOrderIds)
                {
                    Console.WriteLine($"- Order ID: {deliveryId}");
                }
            }
            else
            {
                Console.WriteLine("Driver not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
        WaitForInput();
    }

    private TuberDriverDTO MapToDTO(TuberDriver driver)
    {
        return new TuberDriverDTO
        {
            Id = driver.Id,
            Name = driver.Name,
            DeliveryOrderIds = _orders
                .Where(order => order.TuberDriverId == driver.Id)
                .Select(order => order.Id)
                .ToList()
        };
    }


    private void WaitForInput()
    {
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}
