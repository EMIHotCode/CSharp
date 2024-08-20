using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using static System.Console;

namespace DelegatesAndEvents
{
    public delegate void CarEventHandler();

    public abstract class Car
    {
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public int DrivenDistance { get; set; } = 0;
        public bool carCrash { get; set; } = false;
        public abstract void Drive();

        public class SportCar : Car
        {
            public new int MaxSpeed { get; set; } = 38;

            public event CarEventHandler Finish;  // Событие "финиширование", срабатывает, когда автомобиль прошёл дистанцию

            public event CarEventHandler Crash; // Событие "поломка", срабатывает, когда скорость автомобиля упала до нуля.
            public override void Drive()
            {
                int distancePerSec = new Random().Next(MaxSpeed);
                if (distancePerSec == 0)
                {
                    Crash();
                }
                else
                {
                    DrivenDistance += distancePerSec;

                    if (DrivenDistance >= 100)
                    {
                        Finish();
                    }
                }
            }
        }

        public class PassengerCar : Car
        {
            public new int MaxSpeed { get; set; } = 22;

            public event CarEventHandler Finish;
            public event CarEventHandler Crash;
            public override void Drive()
            {
                int distancePerSec = (new Random()).Next(MaxSpeed);
                if (distancePerSec == 0)
                {
                    Crash();
                }
                else
                {
                    DrivenDistance += distancePerSec;

                    if (DrivenDistance >= 100)
                    {
                        Finish();
                    }
                }
            }
        }


        public class CargoCar : Car
        {
            public new int MaxSpeed { get; set; } = 18;

            public event CarEventHandler Finish;
            public event CarEventHandler Crash;

            public override void Drive()
            {
                int distancePerSec = (new Random()).Next(MaxSpeed);
                if (distancePerSec == 0)
                {
                    Crash();
                }
                else
                {
                    DrivenDistance += distancePerSec;

                    if (DrivenDistance >= 100)
                    {
                        Finish();
                    }
                }
            }
        }


        public class Bus : Car
        {
            public new int MaxSpeed { get; set; } = 16;

            public event CarEventHandler Finish;
            public event CarEventHandler Crash;
            public override void Drive()
            {
                int distancePerSec = (new Random()).Next(MaxSpeed);
                if (distancePerSec == 0)
                {
                    Crash();
                }
                else
                {
                    DrivenDistance += distancePerSec;

                    if (DrivenDistance >= 100)
                    {
                        Finish();
                    }
                }
            }
        }


        public class Game
        {
            protected IEnumerable<Car> _race; //  Список автомобилей гонки
            public Game(IEnumerable<Car> race) // конструктор для гонки
            {
                this._race = race;
            }
            public void Start()
            {
                //цикл задаёт поведение экземпляром из списка автомобилей при срабатывании событий Finish и Crash
                foreach (var item in _race)
                {
                    //для спортивных авто
                    if (item.GetType().Equals(typeof(SportCar)))
                    {
                        (item as SportCar).Finish += () =>
                        {
                            WriteLine($"Спорткар {item.Name} доехал до финиша первым!!!");
                        };
                        (item as SportCar).Crash += () =>
                        {
                            WriteLine($"Спорткар  {item.Name} сломался!!!");
                            item.carCrash = true;
                        };
                    }

                    //для легковушек
                    if (item.GetType().Equals(typeof(PassengerCar)))
                    {
                        (item as PassengerCar).Finish += () =>
                        {
                            WriteLine($"Легковое авто {item.Name} доехало до финиша первым!!!");
                        };
                        (item as PassengerCar).Crash += () =>
                        {
                            WriteLine($"Легковое авто  {item.Name} сломалось!!!");
                            item.carCrash = true;
                        };
                    }

                    //для грузовиков
                    if (item.GetType().Equals(typeof(CargoCar)))
                    {
                        (item as CargoCar).Finish += () =>
                        {
                            WriteLine($"Грузовое авто {item.Name} доехало до финиша первым!!!");
                        };
                        (item as CargoCar).Crash += () =>
                        {
                            WriteLine($"Грузовое авто  {item.Name} сломалось!!!");
                            item.carCrash = true;
                        };
                    }

                    //для автобусов
                    if (item.GetType().Equals(typeof(Bus)))
                    {
                        (item as Bus).Finish += () =>
                        {
                            WriteLine($"Автобус {item.Name} доехал до финиша первым!!!");
                        };
                        (item as Bus).Crash += () =>
                        {
                            WriteLine($"Автобус  {item.Name} сломался!!!");
                            item.carCrash = true;
                        };
                    }
                }

                WriteLine("СТАРТ!\n");

                int countTact = 1; //следующая строчка отчёта 
                int countOfCrash = 0; //количество поломок. Если сравнивается с количеством участников, гонка прекращается
                int countOfFinish = 0; //количество финишировавших. Если >0, гонка прекращается

                do
                {
                    WriteLine($"Отрезок времени: {countTact++}");
                    foreach (var item in _race)
                    {
                        Write($"{item.Name}\tпройдено {item.DrivenDistance} из 100\n");
                    }
                    WriteLine();

                    //для каждого экземпляра из списка участников гонки вызываем метод Drive, проверяя срабаывание событий Finish и Crash
                    foreach (var item in _race)
                    {
                        if (!item.carCrash)
                        {
                            item.Drive();
                            if (item.carCrash)
                            {
                                countOfCrash++;
                            }

                        }
                        if (item.DrivenDistance >= 100)
                        {
                            countOfFinish++;
                            WriteLine($"Гонку выиграл {item.Name} на {countTact} отрезке времени!");
                            break;
                        }
                    }

                    //останавливаем цикл do-while, когда кто-нибудь финишировал, либо когда все сломались
                    if (countOfFinish > 0)
                    {
                        Stop();
                        break;
                    }
                    if (countOfCrash == _race.Count())
                    {
                        Stop();
                        WriteLine("Никто не сумел добраться до финиша...");
                        break;
                    }
                } while (true);
            }

            public void Stop()
            {
                //цикл задаёт поведение экземпляром из списка автомобилей при срабатывании событий Finish и Crash
                foreach (var item in _race)
                {

                    (item as SportCar).Finish -= null;
                    (item as SportCar).Crash -= null;


                    (item as PassengerCar).Finish -= null;
                    (item as PassengerCar).Crash -= null;


                    (item as CargoCar).Finish -= null;
                    (item as CargoCar).Crash -= null;

                    (item as Bus).Finish -= null;
                    (item as Bus).Crash -= null;

                }
            }
        }
        class Program
        {
            static void Menu()
            {
                Console.WriteLine($"\nИгра \"Автомобильные гонки\"\n\n");
                Console.WriteLine("\t\tМеню");
                Console.WriteLine("1. Создать гонку с заранее заготовленными автомобилями");
                Console.WriteLine("2. Задать автомобили для гонки вручную и запустить соревнование");
                Console.WriteLine("3. ВЫХОД");
                Console.Write("Ваш выбор - ");

            }
            static int checkChoice(int c, int i)
            {
                bool mark = false;
                for (int n = 0; n < i; n++)
                {
                    if (c == n)
                    {
                        mark = true;
                        break;
                    }
                }
                if (!mark)
                {
                    WriteLine("Вы нажали что-то не то. Попробуйте ещё раз.");
                    c = int.Parse(ReadLine());
                    checkChoice(c, i);
                }
                return c;
            }
            static void Main(string[] args)
            {
                List<Car> race = new List<Car>
                    {
                        new PassengerCar() { Name = "Жигули" },
                        new CargoCar() { Name = "КАМАЗ" },
                        new Bus() { Name = "Икарус" },
                        new SportCar() { Name = "BMW" },
                        new Bus() { Name = "ЛиАЗ" }
                    };

                bool exit = false;

                while (!exit)
                {
                    Menu();

                    int c = checkChoice(int.Parse(ReadLine()), 3);
                    switch (c)
                    {
                        case 1:

                            Game game = new Game(race);
                            game.Start();
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 2:
                            string filePath = "top10winners.txt";
                            string[] lines = File.ReadAllLines(filePath); //считываем строки из файла в массив
                            WriteLine();
                            foreach (string s in lines)
                            {
                                WriteLine($"{s}"); //выводим на экран
                            }
                            break;
                        case 3:
                            exit = true;
                            break;

                        default:
                            WriteLine($"Что-то пошло не так...");
                            break;
                    }


                }
                Write("\n\nPress any key...");
                ReadKey();
            }
        }

    }
}