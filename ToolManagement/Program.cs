using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ToolManagement.Models;

namespace ToolManagement
{
    /// <summary>
    /// Główna klasa programu do zarządzania narzędziami.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Ścieżka do pliku z danymi narzędzi.
        /// </summary>
        private static string _path = @"C:\\Users\\admin\\source\\repos\\ToolManagement\\ToolManagement\\tools-data.json";

        /// <summary>
        /// Główna metoda programu.
        /// </summary>
        /// <param name="args">Argumenty wiersza poleceń.</param>
        static void Main(string[] args)
        {
            List<Tool> tools = LoadTools();
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
                    "Wyjdź"
                };

                int selectedIndex = DisplayMenu(options);

                switch (selectedIndex)
                {
                    case 0:
                        AddTool(tools);
                        break;
                    case 1:
                        UpdateTool(tools);
                        break;
                    case 2:
                        RemoveTool(tools);
                        break;
                    case 3:
                        SaveTools(tools);
                        break;
                    case 4:
                        DisplaySingleTool(tools);
                        break;
                    case 5:
                        DisplayAllTools(tools);
                        break;
                    case 6:
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

        /// <summary>
        /// Wyświetla menu i obsługuje nawigację za pomocą strzałek.
        /// </summary>
        /// <param name="options">Opcje menu.</param>
        /// <returns>Indeks wybranej opcji.</returns>
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

        /// <summary>
        /// Wczytuje narzędzia z pliku JSON.
        /// </summary>
        /// <returns>Lista narzędzi.</returns>
        static List<Tool> LoadTools()
        {
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                return JsonSerializer.Deserialize<List<Tool>>(json);
            }
            return new List<Tool>();
        }

        /// <summary>
        /// Zapisuje narzędzia do pliku JSON.
        /// </summary>
        /// <param name="tools">Lista narzędzi do zapisania.</param>
        static void SaveTools(List<Tool> tools)
        {
            string json = JsonSerializer.Serialize(tools);
            File.WriteAllText(_path, json);
            Console.WriteLine("Narzędzia zapisane");
        }

        /// <summary>
        /// Dodaje nowe narzędzie do listy narzędzi.
        /// </summary>
        /// <param name="tools">Lista narzędzi.</param>
        static void AddTool(List<Tool> tools)
        {
            Console.Clear();
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

        /// <summary>
        /// Aktualizuje istniejące narzędzie na liście narzędzi.
        /// </summary>
        /// <param name="tools">Lista narzędzi.</param>
        static void UpdateTool(List<Tool> tools)
        {
            Console.Clear();
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

        /// <summary>
        /// Usuwa narzędzie z listy narzędzi.
        /// </summary>
        /// <param name="tools">Lista narzędzi.</param>
        static void RemoveTool(List<Tool> tools)
        {
            Console.Clear();
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

        /// <summary>
        /// Wyświetla pojedyncze narzędzie na podstawie jego ID.
        /// </summary>
        /// <param name="tools">Lista narzędzi.</param>
        static void DisplaySingleTool(List<Tool> tools)
        {
            Console.Clear();
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

        /// <summary>
        /// Wyświetla wszystkie narzędzia.
        /// </summary>
        /// <param name="tools">Lista narzędzi.</param>
        static void DisplayAllTools(List<Tool> tools)
        {
            Console.Clear();
            foreach (var tool in tools)
            {
                Console.WriteLine($"Id: {tool.Id}, Nazwa: {tool.Name}, Ilość: {tool.Quantity}, Cena: {tool.Price}");
            }
        }
    }
}