using TuberTreats.Models;
using TuberTreats.Menus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var drivers = new List<TuberDriver>
{
    new TuberDriver { Id = 1, Name = "Sophia Harper" },
    new TuberDriver { Id = 2, Name = "Liam Bennett" },
    new TuberDriver { Id = 3, Name = "Ava Carter" }
};

var customers = new List<Customer>
{
    new Customer { Id = 1, Name = "John Doe", Address = "123 Potato Lane" },
    new Customer { Id = 2, Name = "Jane Smith", Address = "456 Sweet St" },
    new Customer { Id = 3, Name = "Alice Johnson", Address = "789 Fry Ave" },
    new Customer { Id = 4, Name = "Bob Brown", Address = "101 Mashed Rd" },
    new Customer { Id = 5, Name = "Charlie Davis", Address = "202 Hash Blvd" }
};

var toppings = new List<Topping>
{
    new Topping { Id = 1, Name = "Cheese" },
    new Topping { Id = 2, Name = "Sour Cream" },
    new Topping { Id = 3, Name = "Bacon Bits" },
    new Topping { Id = 4, Name = "Chives" },
    new Topping { Id = 5, Name = "Butter" },
    new Topping { Id = 6, Name = "Onions" },
    new Topping { Id = 7, Name = "Salt" }
};

var orders = new List<TuberOrder>
{
    new TuberOrder
    {
        Id = 1,
        OrderPlacedOnDate = DateTime.Now.AddDays(-2),
        CustomerId = 1,
        Customer = customers[0],
        TuberDriverId = 1,
        TuberDriver = drivers[0],
        DeliveredOnDate = null,
        TuberToppings = new List<TuberTopping>
        {
            new TuberTopping { Id = 1, TuberOrderId = 1, ToppingId = 1, Topping = toppings[0] },
            new TuberTopping { Id = 2, TuberOrderId = 1, ToppingId = 2, Topping = toppings[1] }
        }
    },
    new TuberOrder
    {
        Id = 2,
        OrderPlacedOnDate = DateTime.Now.AddDays(-1),
        CustomerId = 2,
        Customer = customers[1],
        TuberDriverId = 2,
        TuberDriver = drivers[1],
        DeliveredOnDate = DateTime.Now,
        TuberToppings = new List<TuberTopping>
        {
            new TuberTopping { Id = 3, TuberOrderId = 2, ToppingId = 3, Topping = toppings[2] },
            new TuberTopping { Id = 4, TuberOrderId = 2, ToppingId = 4, Topping = toppings[3] }
        }
    },
    new TuberOrder
    {
        Id = 3,
        OrderPlacedOnDate = DateTime.Now.AddDays(-3),
        CustomerId = 3,
        Customer = customers[2],
        TuberDriverId = null,
        TuberDriver = null,
        DeliveredOnDate = null,
        TuberToppings = new List<TuberTopping>
        {
            new TuberTopping { Id = 5, TuberOrderId = 3, ToppingId = 5, Topping = toppings[4] },
            new TuberTopping { Id = 6, TuberOrderId = 3, ToppingId = 6, Topping = toppings[5] },
            new TuberTopping { Id = 7, TuberOrderId = 3, ToppingId = 7, Topping = toppings[6] }
        }
    }
};

foreach (var customer in customers)
{
    customer.TuberOrders = orders.Where(o => o.CustomerId == customer.Id).ToList();
}

var tuberOrdersMenu = new TuberOrdersMenu(orders, customers, drivers);
var toppingsMenu = new ToppingsMenu(toppings);
var tuberToppingMenu = new TuberToppingMenu(orders, toppings);
var customersMenu = new CustomersMenu(customers);
var tuberDriversMenu = new TuberDriverMenu(drivers, orders);

var mainMenu = new MainMenu(
    tuberOrdersMenu,
    toppingsMenu,
    tuberToppingMenu,
    customersMenu,
    tuberDriversMenu
);

mainMenu.ShowMenu();

app.Run();

// Don't touch or move this!
public partial class Program { }
