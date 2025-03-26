using System;
using System.Collections.Generic;
using ToolManagement.Models;

namespace ToolManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tool> tools = ToolManager.LoadTools();
            List<Customer> customers = CustomerManager.LoadCustomers();
            List<Order> orders = CustomerManager.LoadOrders();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Witaj w menadżerze narzędzi");
                string[] options = {
                    "Dodaj narzędzie",
                    "Aktualizuj narzędzie",
                    "Usuń narzędzie",
                    "Zapisz narzędzia",
                    "Wyświetl pojedyncze narzędzie",
                    "Wyświetl wszystkie narzędzia",
                    "Panel klienta",
                    "Wyjdź"
                };

                int selectedIndex = DisplayMenu(options);

                switch (selectedIndex)
                {
                    case 0:
                        ToolManager.AddTool(tools);
                        break;
                    case 1:
                        ToolManager.UpdateTool(tools);
                        break;
                    case 2:
                        ToolManager.RemoveTool(tools);
                        break;
                    case 3:
                        ToolManager.SaveTools(tools);
                        break;
                    case 4:
                        ToolManager.DisplaySingleTool(tools);
                        break;
                    case 5:
                        ToolManager.DisplayAllTools(tools);
                        break;
                    case 6:
                        CustomerPanel(tools, customers, orders);
                        break;
                    case 7:
                        exit = true;
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                }
            }
        }

        static int DisplayMenu(string[] options)
        {
            int selectedIndex = 0;

            ConsoleKeyInfo keyInfo;
            do
            {
                Console.Clear();
                Console.WriteLine("Witaj w menadżerze narzędzi");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }

                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            return selectedIndex;
        }

        static void CustomerPanel(List<Tool> tools, List<Customer> customers, List<Order> orders)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Panel klienta");
                string[] options = {
                    "Dodaj klienta",
                    "Zamów narzędzie",
                    "Wyświetl wszystkich klientów",
                    "Wyświetl zamówienia klienta",
                    "Wyjdź"
                };

                int selectedIndex = DisplayMenu(options);

                switch (selectedIndex)
                {
                    case 0:
                        CustomerManager.AddCustomer(customers);
                        break;
                    case 1:
                        CustomerManager.OrderTool(tools, customers, orders);
                        break;
                    case 2:
                        CustomerManager.DisplayAllCustomers(customers);
                        break;
                    case 3:
                        CustomerManager.DisplayCustomerOrders(customers, orders, tools);
                        break;
                    case 4:
                        exit = true;
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                }
            }
        }
    }
}