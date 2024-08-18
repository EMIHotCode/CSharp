using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Programm
{
    class Visitor
    {
        [Required]
        [Range(1, 300000, ErrorMessage = "ID должен быть положительным целым числом от 1 до 300000.")]
        public int? Id { get; set; }

        [Required]
        [Range(2000, 2025, ErrorMessage = "Число года должно лежать в пределах от 2000 до 2025 ")]
        public int? Year { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Диапазон ввода числа месяца от 1 до 12")]
        public int? Month { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Время занятий в часах должно находиться в диапазоне от 0 до 10000")]
        public int? OverallTrainingTime { get; set; }

        public override string ToString()
        {
            return $"Клиент ID - {Id} Год\\месяц - {Year}\\{Month} Продолжительность занятий (в часах) - {OverallTrainingTime}";
        }

        public void Show(List<Visitor> visitors)
        {
            foreach (var visitor in visitors)
            {
                Console.WriteLine(visitor.ToString());
            }
        }

        public IEnumerable<Visitor> Sort(List<Visitor> visitors)
        {
            var sortMinTrainingTime = from v in visitors
                                      orderby v.OverallTrainingTime, v.Id descending
                                      select v;
            return sortMinTrainingTime;
        }

    }

    internal class Program
    {
        static void Menu()
        {
            Console.WriteLine($"\nЗадача 3\nО клиентах фитнес-центра\n\n");
            Console.WriteLine("\t\tМеню");
            Console.WriteLine("1. Использовать заранее подготовленный массив клиентов");
            Console.WriteLine("2. Ввести клиентов в последовательность вручную");
            Console.WriteLine("3. ВЫХОД");
        }
        static void Main(string[] args)
        {
            bool exit = false;
            int choise;

            while (!exit)
            {
                Menu();

                Console.Write("Ваш выбор: ");
                Int32.TryParse(Console.ReadLine(), out choise);
                switch (choise)
                {
                    case 1:
                        List<Visitor> visitors = new List<Visitor>
                        {
                            new Visitor {Id = 1, Year = 2024, Month = 10, OverallTrainingTime = 30 },
                            new Visitor {Id = 2, Year = 2023, Month = 11, OverallTrainingTime = 35 },
                            new Visitor {Id = 3, Year = 2022, Month = 9, OverallTrainingTime = 36 },
                            new Visitor {Id = 4, Year = 2021, Month = 8, OverallTrainingTime = 30 },
                            new Visitor {Id = 5, Year = 2020, Month = 7, OverallTrainingTime = 30 },
                            new Visitor {Id = 6, Year = 2019, Month = 6, OverallTrainingTime = 35 },
                        };
                        Console.WriteLine();
                        Visitor myVisitors = new Visitor();
                        myVisitors.Show(visitors);

                        var firstMinVisitor = myVisitors.Sort(visitors).First();
                            Console.WriteLine($"\nID клиента с минимальной продолжительностью занятий: {firstMinVisitor.Id}\n" +
                                $"Продолжительность:{firstMinVisitor.OverallTrainingTime} Год\\месяц: {firstMinVisitor.Year}\\{firstMinVisitor.Month}");
                        break;


                    case 2:
                        List<Visitor> userVisitors = new List<Visitor> {};

                        int id = 1, year, month, trainingTime; // значения по умолчанию
                        while(true)
                        {
                            Console.WriteLine($"\nКлиент №{id} ");

                            Console.Write("Введите год целое число от 2000 до 2025 : ");
                            int.TryParse(Console.ReadLine(), out year);

                            Console.Write("Введите месяц целое число от 1 до 12 : ");
                            int.TryParse(Console.ReadLine(), out month);

                            Console.Write("Введите продолжительность занятий в часах (от 1 до 10000) : ");
                            int.TryParse(Console.ReadLine(), out trainingTime);

                            Visitor temp = new Visitor() { Id = id, Year = year, Month = month, OverallTrainingTime = trainingTime };

                            var context = new ValidationContext(temp);
                            var results = new List<ValidationResult>();
                            if (!Validator.TryValidateObject(temp, context, results, true))
                            {
                                foreach (var error in results)
                                {
                                    Console.WriteLine(error.ErrorMessage);
                                }
                                continue;
                            }
                            else
                                Console.WriteLine($"Клиент №{id} успешно создан. ");
                            userVisitors.Add(new Visitor { Id = id, Year = year, Month = month, OverallTrainingTime = trainingTime });
                            id++;


                            Console.Write("\nПробел - ввести еще одного посетителя клуба вручную, ESC - Выход: ");
                            var keyUserChoice = Console.ReadKey();
                            if (keyUserChoice.Key == ConsoleKey.Escape)
                                break;

                        }
                        Console.WriteLine();
                        Visitor userVisitor = new Visitor();
                        userVisitor.Show(userVisitors);

                        var user_firstMinVisitor = userVisitor.Sort(userVisitors).First();
                        Console.WriteLine($"\nID клиента с минимальной продолжительностью занятий: {user_firstMinVisitor.Id}\n" +
                            $"Продолжительность:{user_firstMinVisitor.OverallTrainingTime} Год\\месяц: {user_firstMinVisitor.Year}\\{user_firstMinVisitor.Month}");
                        break;


                    case 3:
                        exit = true;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("\nВведено неверное значение.\n");
                        Console.ReadKey();
                        break;
                }

                Console.Write("\nПробел - продолжить работу с программой, ESC - Выход: ");
                var key = Console.ReadKey();
                Console.Clear();
                if (key.Key == ConsoleKey.Escape)
                    exit = true;


            }
        }
    }
}
