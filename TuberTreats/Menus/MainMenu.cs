namespace TuberTreats.Menus;

public class MainMenu
{
    private readonly TuberOrdersMenu _tuberOrdersMenu;
    private readonly ToppingsMenu _toppingsMenu;
    private readonly TuberToppingMenu _tuberToppingsMenu;
    private readonly CustomersMenu _customersMenu;
    private readonly TuberDriverMenu _tuberDriversMenu;

    public MainMenu(
        TuberOrdersMenu tuberOrdersMenu,
        ToppingsMenu toppingsMenu,
        TuberToppingMenu tuberToppingsMenu,
        CustomersMenu customersMenu,
        TuberDriverMenu tuberDriversMenu)
    {
        _tuberOrdersMenu = tuberOrdersMenu;
        _toppingsMenu = toppingsMenu;
        _tuberToppingsMenu = tuberToppingsMenu;
        _customersMenu = customersMenu;
        _tuberDriversMenu = tuberDriversMenu;
    }

    public void ShowMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. /tuberorders");
            Console.WriteLine("2. /toppings");
            Console.WriteLine("3. /tubertoppings");
            Console.WriteLine("4. /customers");
            Console.WriteLine("5. /tuberdrivers");
            Console.WriteLine("0. Exit");
            Console.Write("Select an endpoint: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _tuberOrdersMenu.ShowMenu();
                    break;
                case "2":
                    _toppingsMenu.ShowMenu();
                    break;
                case "3":
                    _tuberToppingsMenu.ShowMenu();
                    break;
                case "4":
                    _customersMenu.ShowMenu();
                    break;
                case "5":
                    _tuberDriversMenu.ShowMenu();
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
}
