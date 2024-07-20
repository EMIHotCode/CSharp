//using HomeWork6_1.GameUsers;
using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace HomeWork6_1
{
    namespace GameUsers
    {
        class User
        {
            private string Name { get; set; } // имя пользователя 
            private int ProposeNum { get; set; }   // загаданое число пользователем 

            public User()
            {
                Name = "Player";
                ProposeNum = 0;
            }
            public User(string _name, int _proposeNum)
            {
                Name = _name;
                ProposeNum = _proposeNum;
            }
            public int GetNumber()
            {
                return ProposeNum;
            }
            public string GetName()
            {
                return Name;
            }
            public static User InitUser()
            {
                string name;
                int num;
                Console.Write("\nВведите имя игрока: ");
                name = Console.ReadLine();

                while (true)   // проверка если введена буква а не цифра
                {
                    Console.Write("Загадайте число: ");

                    if (Int32.TryParse(Console.ReadLine(), out num))
                        break;
                    else
                    {
                        Console.WriteLine("ОШИБКА. Введите целое число");
                        continue;
                    }
                }

                User newUser = new User(name, num);
                return newUser;
            }
        }
    }

    namespace CompSpace
    {
        class Computer
        {
            private int MinValue { get; set; } // нижняя граница поиска 
            private int MaxValue { get; set; } // верхняя граница поиска 
            public Computer()
            {
                MinValue = 0;
                MaxValue = 0;
            }
            public Computer(int _minVal, int _maxVal)
            {
                MinValue = _minVal;
                MaxValue = _maxVal;
            }

            public void GuessNumber(ref GameUsers.User Gamer)
            {

                if (MinValue > MaxValue)   // проверка что min и max находятся на своих местах
                {
                    (MinValue, MaxValue) = (MaxValue, MinValue);
                }

                int computerRandNum =0;
                int min = MinValue, max = MaxValue;



                Random guessNum = new Random();

                bool exitGame = false;
                Console.WriteLine("\nИгра началась.\n");

                while (!exitGame)
                {
                    if ((MaxValue - MinValue) <= 1)
                    {
                        if (Gamer.GetNumber() == MaxValue) // совпадает с макс числом
                        {
                            Console.WriteLine($"Компьютер: {Gamer.GetNumber()}" ); 
                            Console.WriteLine($"Угадал. Игрок {Gamer.GetName()} загадал число {Gamer.GetNumber()}");
                            break;
                        }
                        if (Gamer.GetNumber() == MinValue) //
                        {
                            Console.WriteLine($"Компьютер: {Gamer.GetNumber()}");
                            Console.WriteLine($"Угадал. Игрок {Gamer.GetName()} загадал число {Gamer.GetNumber()}");
                            break;
                        }
                        else if (Gamer.GetNumber() < MinValue)  // выходит за пределы минимума
                        {
                            if (computerRandNum == MaxValue)
                            {
                                Console.WriteLine($"Компьютер: {MinValue}");
                                Console.WriteLine($"{Gamer.GetName()}: Меньше");
                                Console.WriteLine("Компьютер понял Число выходит за границы поиска значений");
                            }
                            else
                            {
                                Console.WriteLine("Компьютер понял Число выходит за границы поиска значений");
                            }
                            break;
                        }
                        else if (Gamer.GetNumber() > MinValue + 1) 
                        {
                            Console.WriteLine($"Компьютер: {max}");
                            Console.WriteLine($"{Gamer.GetName()}: Больше");
                            Console.WriteLine("Компьютер понял Число выходит за границы поиска значений");
                            break;
                        }
                    }


                    bool exit = false;

                    while (!exit)
                    {
                        computerRandNum = guessNum.Next(MinValue, MaxValue); // генерируем целое число из диапазона 
                        if (computerRandNum == MaxValue || computerRandNum == MinValue)
                            continue;
                        else
                            exit = true;
                    }


                    Console.WriteLine($"Компьютер: {computerRandNum}");


                    if (computerRandNum == Gamer.GetNumber())   //условие выхода из программы если найдено нужное число в диапазоне
                    {
                        Console.WriteLine($"Угадал. Игрок {Gamer.GetName()} загадал число {Gamer.GetNumber()}");
                        break;
                    }
                    else
                    {
                        if (computerRandNum < Gamer.GetNumber())
                        {
                            Console.WriteLine($"{Gamer.GetName()}: Больше");
                            MinValue = computerRandNum;
                        }
                        else
                        {
                            Console.WriteLine($"{Gamer.GetName()}: Meньше");
                            MaxValue = computerRandNum;
                        }
                    }
                }
            }

            public static Computer InitBorder()
            {
                int min;
                int max;
                while (true)   // проверка если введена буква а не цифра
                {
                    Console.Write("\nВведите нижнюю границу поиска: ");

                    if (int.TryParse(Console.ReadLine(), out min))
                        break;
                    else
                    {
                        Console.WriteLine("ОШИБКА. Введите целое число");
                        continue;
                    }
                }

                while (true)   // проверка если введена буква а не цифра
                {
                    Console.Write("\nВведите верхнюю границу поиска: ");

                    if (int.TryParse(Console.ReadLine(), out max))
                        break;
                    else
                    {
                        Console.WriteLine("ОШИБКА. Введите целое число");
                        continue;
                    }
                }
                Computer newUser = new Computer(min, max);
                return newUser;
            }
        }
   

       
    }


    internal class Program
    {

        static void Main(string[] args)
        {
            bool exit = false;
            int choise;


            while (!exit)
            {
            Console.WriteLine("\tПрограмма \"Угадай число\". Пользователь вводит число и диапазон поиска." +
                "Компьютер угадывает.\n\n");
                Console.WriteLine("\t\tМеню");
                Console.WriteLine("1. Начать игру. Ввести имя игрока, число и диапазон поиска.");
                Console.WriteLine("2. ВЫХОД");
                Console.Write("Ваш выбор: ");
                Int32.TryParse(Console.ReadLine(), out choise);

                switch (choise)
                {
                    case 1:
                    GameUsers.User Gamer = GameUsers.User.InitUser();

                    CompSpace.Computer PlayGame = CompSpace.Computer.InitBorder();
                    PlayGame.GuessNumber(ref Gamer);
                    break;

                    case 2:
                        exit = true;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("\nВведено неверное значение. Выход из программы.\n");
                        Console.ReadKey();
                        return;
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