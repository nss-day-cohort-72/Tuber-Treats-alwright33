using TuberTreats.Models;
using TuberTreats.Models.DTOs;

public class TuberOrdersMenu
{
    private readonly List<TuberOrder> _orders;
    private readonly List<Customer> _customers;
    private readonly List<TuberDriver> _drivers;

    public TuberOrdersMenu(List<TuberOrder> orders, List<Customer> customers, List<TuberDriver> drivers)
    {
        _orders = orders;
        _customers = customers;
        _drivers = drivers;
    }

    public void ShowMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== /tuberorders Menu ===");
            Console.WriteLine("1. Get all orders");
            Console.WriteLine("2. Get an order by ID");
            Console.WriteLine("3. Submit a new order");
            Console.WriteLine("4. Assign a driver to an order");
            Console.WriteLine("5. Complete an order");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Select an action: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllOrders();
                    break;
                case "2":
                    DisplayOrderById();
                    break;
                case "3":
                    SubmitNewOrder();
                    break;
                case "4":
                    AssignDriverToOrder();
                    break;
                case "5":
                    CompleteOrder();
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

    private void DisplayAllOrders()
    {
        Console.Clear();
        Console.WriteLine("All Orders:");
        var orderDTOs = _orders.Select(MapToDTO);
        foreach (var order in orderDTOs)
        {
            Console.WriteLine($"\nOrder ID: {order.Id}");
            Console.WriteLine($"- Customer: {order.CustomerName}");
            Console.WriteLine($"- Placed On: {order.OrderPlacedOnDate}");
            Console.WriteLine($"- Delivered On: {order.DeliveredOnDate?.ToString() ?? "Not Delivered"}");
            Console.WriteLine($"- Assigned Driver: {order.TuberDriverName ?? "Unassigned"}");
            Console.WriteLine("- Toppings:");
            foreach (var topping in order.Toppings)
            {
                Console.WriteLine($"  - {topping.ToppingName}");
            }
        }
        WaitForInput();
    }


    private void DisplayOrderById()
    {
        Console.Clear();
        Console.Write("Enter Order ID: ");
        if (int.TryParse(Console.ReadLine(), out int orderId))
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                var orderDTO = MapToDTO(order);
                Console.WriteLine($"\nOrder ID: {orderDTO.Id}");
                Console.WriteLine($"- Customer: {orderDTO.CustomerName}");
                Console.WriteLine($"- Placed On: {orderDTO.OrderPlacedOnDate}");
                Console.WriteLine($"- Delivered On: {orderDTO.DeliveredOnDate?.ToString() ?? "Not Delivered"}");
                Console.WriteLine($"- Assigned Driver: {orderDTO.TuberDriverName ?? "Unassigned"}");
                Console.WriteLine("- Toppings:");
                foreach (var topping in orderDTO.Toppings)
                {
                    Console.WriteLine($"  - {topping.ToppingName}");
                }
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
        WaitForInput();
    }


    private void SubmitNewOrder()
    {
        Console.Clear();
        Console.Write("Enter Customer ID: ");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            var customer = _customers.FirstOrDefault(c => c.Id == customerId);
            if (customer != null)
            {
                var newOrder = new TuberOrder
                {
                    Id = _orders.Max(o => o.Id) + 1,
                    OrderPlacedOnDate = DateTime.Now,
                    CustomerId = customerId,
                    Customer = customer,
                    TuberToppings = new List<TuberTopping>()
                };
                _orders.Add(newOrder);

                var newOrderDTO = MapToDTO(newOrder);
                Console.WriteLine($"Order created successfully! Order ID: {newOrderDTO.Id}, Customer: {newOrderDTO.CustomerName}");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
        WaitForInput();
    }

    private void AssignDriverToOrder()
    {
        Console.Clear();
        Console.Write("Enter Order ID: ");
        if (int.TryParse(Console.ReadLine(), out int orderId))
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                Console.Write("Enter Driver ID: ");
                if (int.TryParse(Console.ReadLine(), out int driverId))
                {
                    var driver = _drivers.FirstOrDefault(d => d.Id == driverId);
                    if (driver != null)
                    {
                        order.TuberDriverId = driverId;
                        order.TuberDriver = driver;

                        var updatedOrderDTO = MapToDTO(order);
                        Console.WriteLine($"Driver assigned successfully! Driver: {updatedOrderDTO.TuberDriverName}");
                    }
                    else
                    {
                        Console.WriteLine("Driver not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for Driver ID.");
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

    private void CompleteOrder()
    {
        Console.Clear();
        Console.Write("Enter Order ID: ");
        if (int.TryParse(Console.ReadLine(), out int orderId))
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.DeliveredOnDate = DateTime.Now;

                var completedOrderDTO = MapToDTO(order);
                Console.WriteLine($"Order marked as completed! Order ID: {completedOrderDTO.Id}, Delivered On: {completedOrderDTO.DeliveredOnDate}");
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

    private TuberOrderDTO MapToDTO(TuberOrder order)
    {
        return new TuberOrderDTO
        {
            Id = order.Id,
            OrderPlacedOnDate = order.OrderPlacedOnDate,
            CustomerId = order.CustomerId,
            CustomerName = order.Customer?.Name,
            TuberDriverId = order.TuberDriverId,
            TuberDriverName = order.TuberDriver?.Name,
            DeliveredOnDate = order.DeliveredOnDate,
            Toppings = order.TuberToppings.Select(tt => new TuberToppingDTO
            {
                Id = tt.Id,
                ToppingId = tt.ToppingId,
                ToppingName = tt.Topping?.Name
            }).ToList()
        };
    }

    private void WaitForInput()
    {
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}
