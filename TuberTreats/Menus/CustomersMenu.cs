using TuberTreats.Models;
using TuberTreats.Models.DTOs;

namespace TuberTreats.Menus;

public class CustomersMenu
{
    private readonly List<Customer> _customers;

    public CustomersMenu(List<Customer> customers)
    {
        _customers = customers;
    }

    public void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== /customers Menu ===");
        Console.WriteLine("1. Get all Customers");
        Console.WriteLine("2. Get a Customer by ID");
        Console.WriteLine("3. Add a Customer");
        Console.WriteLine("4. Delete a Customer");
        Console.WriteLine("0. Return to Main Menu");
        Console.Write("Select an action: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                DisplayAllCustomers();
                break;
            case "2":
                DisplayCustomerById();
                break;
            case "3":
                AddCustomer();
                break;
            case "4":
                DeleteCustomer();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid choice. Press any key to try again...");
                Console.ReadKey();
                break;
        }
    }

    private void DisplayAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("All Customers:");
        var customerDTOs = _customers.Select(customer => new CustomerDTO
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            OrderIds = customer.TuberOrders?.Select(order => order.Id).ToList() ?? new List<int>()
        }).ToList();

        foreach (var customerDTO in customerDTOs)
        {
            Console.WriteLine($"Customer ID: {customerDTO.Id}, Name: {customerDTO.Name}, Address: {customerDTO.Address}");
        }
        WaitForInput();
    }

    private void DisplayCustomerById()
    {
        Console.Clear();
        Console.Write("Enter Customer ID: ");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            var customer = _customers.FirstOrDefault(c => c.Id == customerId);
            if (customer != null)
            {
                if (customer.TuberOrders == null || !customer.TuberOrders.Any())
                {
                    Console.WriteLine("No orders found for this customer.");
                }
                else
                {
                    var customerDTO = new CustomerDTO
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Address = customer.Address,
                        OrderIds = customer.TuberOrders.Select(order => order.Id).ToList()
                    };

                    Console.WriteLine($"Customer ID: {customerDTO.Id}, Name: {customerDTO.Name}, Address: {customerDTO.Address}");
                    Console.WriteLine("Orders:");
                    foreach (var order in customer.TuberOrders)
                    {
                        var toppings = order.TuberToppings?.Select(t => t.Topping?.Name).ToList() ?? new List<string>();
                        var assignedDriver = order.TuberDriver?.Name ?? "Unassigned";

                        Console.WriteLine($"\nOrder ID: {order.Id}");
                        Console.WriteLine($"- Order Placed On: {order.OrderPlacedOnDate}");
                        Console.WriteLine($"- Delivered On: {order.DeliveredOnDate?.ToString() ?? "Not Delivered"}");
                        Console.WriteLine($"- Assigned Driver: {assignedDriver}");
                        Console.WriteLine("- Toppings:");
                        foreach (var topping in toppings)
                        {
                            Console.WriteLine($"  - {topping}");
                        }
                    }
                }
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


    private void AddCustomer()
    {
        Console.Clear();
        Console.Write("Enter Customer Name: ");
        var customerName = Console.ReadLine();
        Console.Write("Enter Customer Address: ");
        var customerAddress = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(customerName) && !string.IsNullOrWhiteSpace(customerAddress))
        {
            var newCustomer = new Customer
            {
                Id = _customers.Max(c => c.Id) + 1,
                Name = customerName,
                Address = customerAddress,
                TuberOrders = new List<TuberOrder>()
            };
            _customers.Add(newCustomer);

            var customerDTO = new CustomerDTO
            {
                Id = newCustomer.Id,
                Name = newCustomer.Name,
                Address = newCustomer.Address,
                OrderIds = new List<int>()
            };

            Console.WriteLine($"Customer added successfully! Customer ID: {customerDTO.Id}, Name: {customerDTO.Name}, Address: {customerDTO.Address}");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
        WaitForInput();
    }

    private void DeleteCustomer()
    {
        Console.Clear();
        Console.Write("Enter Customer ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int deleteCustomerId))
        {
            var customer = _customers.FirstOrDefault(c => c.Id == deleteCustomerId);
            if (customer != null)
            {
                var customerDTO = new CustomerDTO
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    OrderIds = customer.TuberOrders?.Select(order => order.Id).ToList() ?? new List<int>()
                };

                _customers.Remove(customer);
                Console.WriteLine($"Customer deleted successfully! Customer ID: {customerDTO.Id}, Name: {customerDTO.Name}");
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

    private void WaitForInput()
    {
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}
