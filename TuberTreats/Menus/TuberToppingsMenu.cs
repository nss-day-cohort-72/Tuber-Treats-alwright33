using TuberTreats.Models;
using TuberTreats.Models.DTOs;

namespace TuberTreats.Menus;

public class TuberToppingMenu
{
    private readonly List<TuberOrder> _orders;
    private readonly List<Topping> _toppings;

    public TuberToppingMenu(List<TuberOrder> orders, List<Topping> toppings)
    {
        _orders = orders;
        _toppings = toppings;
    }

    public void ShowMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== /tubertoppings Menu ===");
            Console.WriteLine("1. Get all TuberToppings");
            Console.WriteLine("2. Add a Topping to a TuberOrder");
            Console.WriteLine("3. Remove a Topping from a TuberOrder");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Select an action: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllTuberToppings();
                    break;
                case "2":
                    AddToppingToOrder();
                    break;
                case "3":
                    RemoveToppingFromOrder();
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

    private void DisplayAllTuberToppings()
    {
        Console.Clear();
        Console.WriteLine("All TuberToppings:");
        foreach (var tuberTopping in _orders.SelectMany(o => o.TuberToppings))
        {
            var dto = new TuberToppingDTO
            {
                Id = tuberTopping.Id,
                ToppingId = tuberTopping.ToppingId,
                ToppingName = tuberTopping.Topping?.Name
            };

            Console.WriteLine($"TuberTopping ID: {dto.Id}, Topping: {dto.ToppingName}");
        }
        WaitForInput();
    }

    private void AddToppingToOrder()
    {
        Console.Clear();
        Console.Write("Enter Order ID: ");
        if (int.TryParse(Console.ReadLine(), out int orderId))
        {
            var order = _orders.FirstOrDefault(ord => ord.Id == orderId);
            if (order != null)
            {
                Console.Write("Enter Topping ID: ");
                if (int.TryParse(Console.ReadLine(), out int toppingId))
                {
                    var topping = _toppings.FirstOrDefault(top => top.Id == toppingId);
                    if (topping != null)
                    {
                        var newTopping = new TuberTopping
                        {
                            Id = _orders.SelectMany(ord => ord.TuberToppings).Max(TubeTop => TubeTop.Id) + 1,
                            TuberOrderId = orderId,
                            ToppingId = toppingId,
                            Topping = topping
                        };
                        order.TuberToppings.Add(newTopping);

                        var dto = new TuberToppingDTO
                        {
                            Id = newTopping.Id,
                            ToppingId = newTopping.ToppingId,
                            ToppingName = newTopping.Topping?.Name
                        };

                        Console.WriteLine($"Topping added successfully! TuberTopping ID: {dto.Id}, Topping: {dto.ToppingName}");
                    }
                    else
                    {
                        Console.WriteLine("Topping not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for Topping ID.");
                }
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input for Order ID.");
        }
        WaitForInput();
    }

    private void RemoveToppingFromOrder()
    {
        Console.Clear();
        Console.Write("Enter TuberTopping ID to remove: ");
        if (int.TryParse(Console.ReadLine(), out int tuberToppingId))
        {
            var tuberTopping = _orders.SelectMany(o => o.TuberToppings).FirstOrDefault(tt => tt.Id == tuberToppingId);
            if (tuberTopping != null)
            {
                var order = _orders.First(o => o.Id == tuberTopping.TuberOrderId);
                order.TuberToppings.Remove(tuberTopping);

                var dto = new TuberToppingDTO
                {
                    Id = tuberTopping.Id,
                    ToppingId = tuberTopping.ToppingId,
                    ToppingName = tuberTopping.Topping?.Name
                };

                Console.WriteLine($"Topping removed successfully! TuberTopping ID: {dto.Id}, Topping: {dto.ToppingName}");
            }
            else
            {
                Console.WriteLine("TuberTopping not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input for TuberTopping ID.");
        }
        WaitForInput();
    }

    private void WaitForInput()
    {
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }
}
