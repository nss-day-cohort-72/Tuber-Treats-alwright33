using TuberTreats.Models;
using TuberTreats.Models.DTOs;

namespace TuberTreats.Menus;

public class ToppingsMenu
{
    private readonly List<Topping> _toppings;

    public ToppingsMenu(List<Topping> toppings)
    {
        _toppings = toppings;
    }

    public void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== /toppings Menu ===");
        Console.WriteLine("1. Get all toppings");
        Console.WriteLine("2. Get topping by ID");
        Console.WriteLine("0. Return to Main Menu");
        Console.Write("Select an action: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                GetAllToppings();
                break;
            case "2":
                GetToppingById();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid choice. Press any key to try again...");
                Console.ReadKey();
                break;
        }
    }

    private void GetAllToppings()
    {
        Console.Clear();
        Console.WriteLine("All Toppings:");
        var toppingDTOs = _toppings.Select(t => new ToppingDTO
        {
            Id = t.Id,
            Name = t.Name
        }).ToList();

        foreach (var topping in toppingDTOs)
        {
            Console.WriteLine($"Topping ID: {topping.Id}, Name: {topping.Name}");
        }

        WaitForInput();
    }

    private void GetToppingById()
    {
        Console.Clear();
        Console.Write("Enter Topping ID: ");
        if (int.TryParse(Console.ReadLine(), out int toppingId))
        {
            var topping = _toppings.FirstOrDefault(t => t.Id == toppingId);
            if (topping != null)
            {
                var toppingDTO = new ToppingDTO
                {
                    Id = topping.Id,
                    Name = topping.Name
                };

                Console.WriteLine($"Topping ID: {toppingDTO.Id}, Name: {toppingDTO.Name}");
            }
            else
            {
                Console.WriteLine("Topping not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        WaitForInput();
    }

    private void WaitForInput()
    {
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}
