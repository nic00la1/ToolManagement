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
            Console.WriteLine("Narzêdzia zapisane");
        }

        public static void AddTool(List<Tool> tools)
        {
            Console.Clear();
            Console.Write("Podaj nazwê narzêdzia: ");
            string name = Console.ReadLine();
            Console.Write("Podaj iloœæ narzêdzi: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Podaj cenê narzêdzia: ");
            decimal price = decimal.Parse(Console.ReadLine());

            if (quantity <= 0 || price <= 0)
            {
                Console.WriteLine("Iloœæ i cena musz¹ byæ liczbami dodatnimi.");
                return;
            }

            int id = tools.Count > 0 ? tools[^1].Id + 1 : 1;
            tools.Add(new Tool { Id = id, Name = name, Quantity = quantity, Price = price });
            Console.WriteLine($"Dodano narzêdzie {name}");
        }

        public static void UpdateTool(List<Tool> tools)
        {
            Console.Clear();
            Console.Write("Podaj id narzêdzia: ");
            int id = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == id);

            if (tool == null)
            {
                Console.WriteLine("Narzêdzie nie znalezione.");
                return;
            }

            Console.Write("Podaj nazwê narzêdzia: ");
            tool.Name = Console.ReadLine();
            Console.Write("Podaj iloœæ narzêdzi: ");
            tool.Quantity = int.Parse(Console.ReadLine());
            Console.Write("Podaj cenê narzêdzia: ");
            tool.Price = decimal.Parse(Console.ReadLine());

            if (tool.Quantity <= 0 || tool.Price <= 0)
            {
                Console.WriteLine("Iloœæ i cena musz¹ byæ liczbami dodatnimi.");
                return;
            }

            Console.WriteLine("Narzêdzie zaktualizowane.");
        }

        public static void RemoveTool(List<Tool> tools)
        {
            Console.Clear();
            Console.Write("Podaj id narzêdzia: ");
            int id = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == id);

            if (tool == null)
            {
                Console.WriteLine("Narzêdzie nie znalezione.");
                return;
            }

            tools.Remove(tool);
            Console.WriteLine($"Usuniêto narzêdzie {tool.Name}");
        }

        public static void DisplaySingleTool(List<Tool> tools)
        {
            Console.Clear();
            Console.Write("Podaj id narzêdzia: ");
            int id = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == id);

            if (tool == null)
            {
                Console.WriteLine("Narzêdzie nie znalezione.");
                return;
            }

            Console.WriteLine($"Id: {tool.Id}, Nazwa: {tool.Name}, Iloœæ: {tool.Quantity}, Cena: {tool.Price}");
        }

        public static void DisplayAllTools(List<Tool> tools)
        {
            Console.Clear();
            foreach (var tool in tools)
            {
                Console.WriteLine($"Id: {tool.Id}, Nazwa: {tool.Name}, Iloœæ: {tool.Quantity}, Cena: {tool.Price}");
            }
            Console.WriteLine("Naciœnij dowolny klawisz, aby kontynuowaæ...");
            Console.ReadKey();
        }
    }
}
