using PrintingShopBusinessLogic;

namespace Printing_Shop_Management_System
{
    public class PrintingShopLogic
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Printing Shop");

            // customers
            List<string> customers = new List<string>();

            // orders (same position in every list = same order)
            List<string> orderCustomers = new List<string>();
            List<string> orderSizes = new List<string>();
            List<int> orderCopies = new List<int>();
            List<string> orderStatuses = new List<string>();

            DisplayOptions();
            string choice = Console.ReadLine();

            while (choice != "0")
            {
                if (choice == "1")
                {
                    AddCustomer(customers);
                }
                else if (choice == "2")
                {
                    CreateOrder(customers, orderCustomers, orderSizes, orderCopies, orderStatuses);
                }
                else if (choice == "3")
                {
                    ViewCustomerOrders(orderCustomers, orderSizes, orderCopies, orderStatuses);
                }
                else if (choice == "4")
                {
                    UpdateOrderStatus(orderCustomers, orderSizes, orderCopies, orderStatuses);
                }
                else if (choice == "5")
                {
                    ViewAllCustomers(customers);
                }
                else
                {
                    Console.WriteLine("Incorrect input...");
                }

                DisplayOptions();
                choice = Console.ReadLine();
            }

            Console.WriteLine("Thank you for using Printing Shop");
        }

        static void DisplayOptions()
        {
            Console.WriteLine("");
            Console.WriteLine("----- Staff / Owner View -----");
            Console.WriteLine("1. Add customer");
            Console.WriteLine("2. Create order");
            Console.WriteLine("4. Update order status");
            Console.WriteLine("5. View all customers");
            Console.WriteLine("");
            Console.WriteLine("----- Customer View -----");
            Console.WriteLine("3. View Customer order");
            Console.WriteLine("");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
        }

        static void AddCustomer(List<string> customers)
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            PrintingShopLogic businesslogic = new PrintingShopLogic();

            if (businesslogic.IsCustomerExisting(customers, name))

            {
                Console.WriteLine("Customer already exists...");
            }
            else
            {
                customers.Add(name);
                Console.WriteLine("");
                Console.WriteLine($"Customer {name} was added.");
            }
        }

        static void CreateOrder (List<string> customers,
                                 List<string> orderCustomers,
                                 List<string> orderSizes,
                                 List<int> orderCopies,
                                 List<string> orderStatuses)
        {
            Console.WriteLine("");
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            PrintingShopLogic businesslogic = new PrintingShopLogic();

            if (businesslogic.IsCustomerExisting(customers, name))
            {
                Console.WriteLine("1. Short");
                Console.WriteLine("2. Long/legal");
                Console.WriteLine("3. A4");
                Console.WriteLine("4. exit");
                Console.WriteLine("");
                Console.Write("Choose the size of paper: ");

                string order = Console.ReadLine();
                string paperSize = "";

                switch (order)
                {
                    case "1":
                        Console.WriteLine("");
                        Console.WriteLine("You have selected Short size paper");
                        paperSize = "Short";
                        break;
                    case "2":
                        Console.WriteLine("");
                        Console.WriteLine("You have selected Long/legal size paper");
                        paperSize = "Long/legal";
                        break;
                    case "3":
                        Console.WriteLine("");
                        Console.WriteLine("You have selected A4 size paper");
                        paperSize = "A4";
                        break;
                    case "4":
                        Console.WriteLine("");
                        Console.WriteLine("You selected exit");
                        break;
                    default:
                        Console.WriteLine("");
                        Console.WriteLine("Incorrect input...");
                        break;
                }

                if (paperSize != "")
                {
                    Console.WriteLine("");
                    Console.Write("Enter number of copies: ");
                    int copies = Convert.ToInt16(Console.ReadLine());

                    int totalPrice = businesslogic.ComputePrice(paperSize, copies);

                    orderCustomers.Add(name);
                    orderSizes.Add(paperSize);
                    orderCopies.Add(copies);
                    orderStatuses.Add("Pending");

                    Console.WriteLine("");
                    Console.WriteLine($"Order created for {name}: {copies} copies, {paperSize}. Total price is {totalPrice}");
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Customer not found. Please add the customer first.");
            }
        }

        static void ViewCustomerOrders(List<string> orderCustomers, List<string> orderSizes, List<int> orderCopies, List<string> orderStatuses)
        {
            Console.WriteLine("");
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            bool hasOrder = false;
            int index = 0;

            while (index < orderCustomers.Count)
            {
                if (orderCustomers[index] == name)
                {
                    int orderNumber = index + 1;
                    Console.WriteLine($"Order #{orderNumber} - {orderSizes[index]} - {orderCopies[index]} copies - {orderStatuses[index]}");
                    hasOrder = true;
                }
                index += 1;
            }

            if (hasOrder == false)
            {
                Console.WriteLine("No orders found for this customer.");
            }
        }

        static void UpdateOrderStatus(List<string> orderCustomers, List<string> orderSizes, List<int> orderCopies, List<string> orderStatuses)
        {
            PrintingShopLogic businesslogic = new PrintingShopLogic();

            // show all orders first so the user knows the order number
            int index = 0;

            while (index < orderCustomers.Count)
            {
                int orderNumber = index + 1;
                Console.WriteLine($"Order #{orderNumber} - {orderCustomers[index]} - {orderSizes[index]} - {orderCopies[index]} copies - {orderStatuses[index]}");
                index += 1;
            }

            Console.Write("Enter the order number to update: ");
            int selectedOrder = Convert.ToInt16(Console.ReadLine());

            if (businesslogic.IsOrderNumberValid(orderCustomers.Count, selectedOrder))
            {
                Console.WriteLine("1. Pending");
                Console.WriteLine("2. Printing");
                Console.WriteLine("3. Done");
                Console.Write("Choose the new status: ");

                string status = Console.ReadLine();

                switch (status)
                {
                    case "1":
                        
                        orderStatuses[selectedOrder - 1] = "Pending";
                        Console.WriteLine("");
                        Console.WriteLine("Order status updated to Pending");
                        break;
                    case "2":
                        orderStatuses[selectedOrder - 1] = "Printing";
                        Console.WriteLine("");
                        Console.WriteLine("Order status updated to Printing");
                        break;
                    case "3":
                        orderStatuses[selectedOrder - 1] = "Done";
                        Console.WriteLine("");
                        Console.WriteLine("Order status updated to Done");
                        break;
                    default:
                        Console.WriteLine("Incorrect input...");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Order number not found.");
            }
        }

        static void ViewAllCustomers(List<string> customers)
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers yet.");
            }
            else
            {
                int index = 0;

                while (index < customers.Count)
                {
                    int number = index + 1;
                    Console.WriteLine($"{number}. {customers[index]}");
                    index += 1;
                }
            }
        }

        public bool IsCustomerExisting(List<string> customers, string name)
        {
            return customers.Contains(name);
        }

        public int ComputePrice(string paperSize, int copies)
        {
            // Example pricing logic
            int pricePerCopy = 0;
            switch (paperSize)
            {
                case "Short":
                    pricePerCopy = 2;
                    break;
                case "Long/legal":
                    pricePerCopy = 3;
                    break;
                case "A4":
                    pricePerCopy = 4;
                    break;
                default:
                    pricePerCopy = 0;
                    break;
            }
            return pricePerCopy * copies;
        }

        public bool IsOrderNumberValid(int orderCount, int selectedOrder)
        {
            return selectedOrder > 0 && selectedOrder <= orderCount;
        }
    }
}