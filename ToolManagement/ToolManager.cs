using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ToolManagement.Models;

namespace ToolManagement
{
    public static class ToolManager
    {
        private static string _toolsPath = @"C:\\Users\\admin\\source\\repos\\ToolManagement\\ToolManagement\\tools-data.json";

        public static List<Tool> LoadTools()
        {
            if (File.Exists(_toolsPath))
            {
                string json = File.ReadAllText(_toolsPath);
                return JsonSerializer.Deserialize<List<Tool>>(json);
            }
            return new List<Tool>();
        }

        public static void SaveTools(List<Tool> tools)
        {
            string json = JsonSerializer.Serialize(tools);
            File.WriteAllText(_toolsPath, json);
            Console.WriteLine("Narz�dzia zapisane");
        }

        public static void AddTool(List<Tool> tools)
        {
            Console.Clear();
            Console.Write("Podaj nazw� narz�dzia: ");
            string name = Console.ReadLine();
            Console.Write("Podaj ilo�� narz�dzi: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Podaj cen� narz�dzia: ");
            decimal price = decimal.Parse(Console.ReadLine());

            if (quantity <= 0 || price <= 0)
            {
                Console.WriteLine("Ilo�� i cena musz� by� liczbami dodatnimi.");
                return;
            }

            int id = tools.Count > 0 ? tools[^1].Id + 1 : 1;
            tools.Add(new Tool { Id = id, Name = name, Quantity = quantity, Price = price });
            Console.WriteLine($"Dodano narz�dzie {name}");
        }

        public static void UpdateTool(List<Tool> tools)
        {
            Console.Clear();
            Console.Write("Podaj id narz�dzia: ");
            int id = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == id);

            if (tool == null)
            {
                Console.WriteLine("Narz�dzie nie znalezione.");
                return;
            }

            Console.Write("Podaj nazw� narz�dzia: ");
            tool.Name = Console.ReadLine();
            Console.Write("Podaj ilo�� narz�dzi: ");
            tool.Quantity = int.Parse(Console.ReadLine());
            Console.Write("Podaj cen� narz�dzia: ");
            tool.Price = decimal.Parse(Console.ReadLine());

            if (tool.Quantity <= 0 || tool.Price <= 0)
            {
                Console.WriteLine("Ilo�� i cena musz� by� liczbami dodatnimi.");
                return;
            }

            Console.WriteLine("Narz�dzie zaktualizowane.");
        }

        public static void RemoveTool(List<Tool> tools)
        {
            Console.Clear();
            Console.Write("Podaj id narz�dzia: ");
            int id = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == id);

            if (tool == null)
            {
                Console.WriteLine("Narz�dzie nie znalezione.");
                return;
            }

            tools.Remove(tool);
            Console.WriteLine($"Usuni�to narz�dzie {tool.Name}");
        }

        public static void DisplaySingleTool(List<Tool> tools)
        {
            Console.Clear();
            Console.Write("Podaj id narz�dzia: ");
            int id = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == id);

            if (tool == null)
            {
                Console.WriteLine("Narz�dzie nie znalezione.");
                return;
            }

            Console.WriteLine($"Id: {tool.Id}, Nazwa: {tool.Name}, Ilo��: {tool.Quantity}, Cena: {tool.Price}");
        }

        public static void DisplayAllTools(List<Tool> tools)
        {
            Console.Clear();
            foreach (var tool in tools)
            {
                Console.WriteLine($"Id: {tool.Id}, Nazwa: {tool.Name}, Ilo��: {tool.Quantity}, Cena: {tool.Price}");
            }
            Console.WriteLine("Naci�nij dowolny klawisz, aby kontynuowa�...");
            Console.ReadKey();
        }
    }
}
