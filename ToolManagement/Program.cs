using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ToolManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tool> tools = LoadTools();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Witaj w menadżerze narzędzi");
                Console.WriteLine("1. Dodaj narzędzie");
                Console.WriteLine("2. Aktualizuj narzędzie");
                Console.WriteLine("3. Usuń narzędzie");
                Console.WriteLine("4. Zapisz narzędzia");
                Console.WriteLine("5. Wyświetl pojedyncze narzędzie");
                Console.WriteLine("6. Wyświetl wszystkie narzędzia");
                Console.WriteLine("7. Wyjdź");
                Console.Write("Wybierz opcję: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddTool(tools);
                        break;
                    case "2":
                        UpdateTool(tools);
                        break;
                    case "3":
                        RemoveTool(tools);
                        break;
                    case "4":
                        SaveTools(tools);
                        break;
                    case "5":
                        DisplaySingleTool(tools);
                        break;
                    case "6":
                        DisplayAllTools(tools);
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja, spróbuj ponownie.");
                        break;
                }
            }
        }

        static List<Tool> LoadTools()
        {
            if (File.Exists("tools-data.json"))
            {
                string json = File.ReadAllText("tools-data.json");
                return JsonSerializer.Deserialize<List<Tool>>(json);
            }
            return new List<Tool>();
        }

        static void SaveTools(List<Tool> tools)
        {
            string json = JsonSerializer.Serialize(tools);
            File.WriteAllText("tools-data.json", json);
            Console.WriteLine("Narzędzia zapisane");
        }

        static void AddTool(List<Tool> tools)
        {
            Console.Write("Podaj nazwę narzędzia: ");
            string name = Console.ReadLine();
            Console.Write("Podaj ilość narzędzi: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Podaj cenę narzędzia: ");
            decimal price = decimal.Parse(Console.ReadLine());

            if (quantity <= 0 || price <= 0)
            {
                Console.WriteLine("Ilość i cena muszą być liczbami dodatnimi.");
                return;
            }

            int id = tools.Count > 0 ? tools[^1].Id + 1 : 1;
            tools.Add(new Tool { Id = id, Name = name, Quantity = quantity, Price = price });
            Console.WriteLine($"Dodano narzędzie {name}");
        }

        static void UpdateTool(List<Tool> tools)
        {
            Console.Write("Podaj id narzędzia: ");
            int id = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == id);

            if (tool == null)
            {
                Console.WriteLine("Narzędzie nie znalezione.");
                return;
            }

            Console.Write("Podaj nazwę narzędzia: ");
            tool.Name = Console.ReadLine();
            Console.Write("Podaj ilość narzędzi: ");
            tool.Quantity = int.Parse(Console.ReadLine());
            Console.Write("Podaj cenę narzędzia: ");
            tool.Price = decimal.Parse(Console.ReadLine());

            if (tool.Quantity <= 0 || tool.Price <= 0)
            {
                Console.WriteLine("Ilość i cena muszą być liczbami dodatnimi.");
                return;
            }

            Console.WriteLine("Narzędzie zaktualizowane.");
        }

        static void RemoveTool(List<Tool> tools)
        {
            Console.Write("Podaj id narzędzia: ");
            int id = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == id);

            if (tool == null)
            {
                Console.WriteLine("Narzędzie nie znalezione.");
                return;
            }

            tools.Remove(tool);
            Console.WriteLine($"Usunięto narzędzie {tool.Name}");
        }

        static void DisplaySingleTool(List<Tool> tools)
        {
            Console.Write("Podaj id narzędzia: ");
            int id = int.Parse(Console.ReadLine());
            Tool tool = tools.Find(t => t.Id == id);

            if (tool == null)
            {
                Console.WriteLine("Narzędzie nie znalezione.");
                return;
            }

            Console.WriteLine($"Id: {tool.Id}, Nazwa: {tool.Name}, Ilość: {tool.Quantity}, Cena: {tool.Price}");
        }

        static void DisplayAllTools(List<Tool> tools)
        {
            foreach (var tool in tools)
            {
                Console.WriteLine($"Id: {tool.Id}, Nazwa: {tool.Name}, Ilość: {tool.Quantity}, Cena: {tool.Price}");
            }
        }
    }

    class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
