using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ToolManagement.Models;

namespace ToolManagement
{
    public static class CustomerManager
    {
        private static string _customersPath = @"C:\\Users\\admin\\source\\repos\\ToolManagement\\ToolManagement\\customers-data.json";
        private static string _ordersPath = @"C:\\Users\\admin\\source\\repos\\ToolManagement\\ToolManagement\\orders-data.json";

        public static List<Customer> LoadCustomers()
        {
            if (File.Exists(_customersPath))
            {
                string json = File.ReadAllText(_customersPath);
                return JsonSerializer.Deserialize<List<Customer>>(json);
            }
            return new List<Customer>();
        }

        public static void SaveCustomers(List<Customer> customers)
        {
            string json = JsonSerializer.Serialize(customers);
            File.WriteAllText(_customersPath, json);
            Console.WriteLine("Klienci zapisani");
        }

        public static List<Order> LoadOrders()
        {
            if (File.Exists(_ordersPath))
            {
                string json = File.ReadAllText(_ordersPath);
                return JsonSerializer.Deserialize<List<Order>>(json);
            }
            return new List<Order>();
        }

        public static void SaveOrders(List<Order> orders)
        {
            string json = JsonSerializer.Serialize(orders);
            File.WriteAllText(_ordersPath, json);
            Console.WriteLine("Zam�wienia zapisane");
        }

        public static void AddCustomer(List<Customer> customers)
        {
            Console.Clear();
            Console.Write("Podaj imi� klienta: ");
            string firstName = Console.ReadLine();
            Console.Write("Podaj nazwisko klienta: ");
            string lastName = Console.ReadLine();

            int id = customers.Count > 0 ? customers[^1].Id + 1 : 1;
            customers.Add(new Customer { Id = id, FirstName = firstName, LastName = lastName });
            Console.WriteLine($"Dodano klienta {firstName} {lastName}");
        }

        public static void OrderTool(List<Tool> tools, List<Customer> customers, List<Order> orders)
        {
            Console.Clear();
            Console.Write("Podaj id klienta: ");
            int customerId = int.Parse(Console.ReadLine());
            Customer customer = customers.Find(c => c.Id == customerId);

            if (customer == null)
            {
                Console.WriteLine("Klient nie znaleziony.");
                return;
            }

            Console.Write("Podaj id narz�dzia: ");
            int toolId = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == toolId);

            if (tool == null)
            {
                Console.WriteLine("Narz�dzie nie znalezione.");
                return;
            }

            Console.Write("Podaj ilo�� do zam�wienia: ");
            int quantity = int.Parse(Console.ReadLine());

            if (quantity <= 0 || quantity > tool.Quantity)
            {
                Console.WriteLine("Nieprawid�owa ilo��.");
                return;
            }

            tool.Quantity -= quantity;
            int orderId = orders.Count > 0 ? orders[^1].Id + 1 : 1;
            orders.Add(new Order { Id = orderId, CustomerId = customerId, ToolId = toolId, Quantity = quantity });
            ToolManager.SaveTools(tools); // Zapisz zmiany w narz�dziach
            SaveOrders(orders); // Zapisz zam�wienia
            Console.WriteLine($"Zam�wiono {quantity} sztuk {tool.Name} dla {customer.FirstName} {customer.LastName}");
        }

        public static void DisplayAllCustomers(List<Customer> customers)
        {
            Console.Clear();
            foreach (var customer in customers)
            {
                Console.WriteLine($"Id: {customer.Id}, Imi�: {customer.FirstName}, Nazwisko: {customer.LastName}");
            }
            Console.WriteLine("Naci�nij dowolny klawisz, aby kontynuowa�...");
            Console.ReadKey();
        }

        public static void DisplayCustomerOrders(List<Customer> customers, List<Order> orders, List<Tool> tools)
        {
            Console.Clear();
            Console.Write("Podaj id klienta: ");
            int customerId = int.Parse(Console.ReadLine());
            Customer customer = customers.Find(c => c.Id == customerId);

            if (customer == null)
            {
                Console.WriteLine("Klient nie znaleziony.");
                return;
            }

            Console.WriteLine($"Zam�wienia dla {customer.FirstName} {customer.LastName}:");
            foreach (var order in orders)
            {
                if (order.CustomerId == customerId)
                {
                    Tool tool = tools.Find(t => t.Id == order.ToolId);
                    Console.WriteLine($"Id zam�wienia: {order.Id}, Narz�dzie: {tool.Name}, Ilo��: {order.Quantity}");
                }
            }
            Console.WriteLine("Naci�nij dowolny klawisz, aby kontynuowa�...");
            Console.ReadKey();
        }
    }
}