using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Programm
{
    public class WorkWithStringArray
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Количество букв в строке должно быть от 2 до 50")]
        public string[] Array;

        public WorkWithStringArray()
        {
            InitialStringArray();
        }
        public WorkWithStringArray(string[] array)
        {
            Array = array;
        }

        public void WorkWithArray()
        {
            Console.WriteLine($" {isValidArray(Array)}");
            IEnumerable<string> arreyForWork = from i in Array
                                               orderby i.Length descending, i
                                               select i;
            Array = arreyForWork.ToArray();

        }

        public void Show()
        {
            string str_q = "";
            foreach (var item in Array)
            {
                str_q += item.ToString() + " ";
            }

            Console.Write($"{str_q}");
        }
        bool exit = false;
        public void InitialStringArray()
        {
            while (!exit)
            {
                bool exitForeach = false;

                Console.Write("Вводите слова для создания массива через ПРОБЕЛ: ");

                string readTerminal = Console.ReadLine().ToUpper().Trim();
                string[] tokens = readTerminal.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Array = new string[tokens.Length];
                int iteration = 0;

                foreach (string s in tokens)
                {
                    if (s.Length >= 2 && s.Length <= 50)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"\rМассив должен состоять из слов длинной от 2 до 50 символов");
                        exitForeach = true;
                        break;
                    }
                }

                if (exitForeach == true)
                {
                    continue;
                }

                foreach (string s in tokens)
                {
                    Array[iteration] = s;
                    iteration++;
                    exit = true;

                }
            }
        }

        public static bool isValidArray(string[] Array)
        {
            bool isValid = true;
            foreach (string s in Array)
            {
                if (string.IsNullOrEmpty(s) || s.Length < 2)
                {
                    isValid = false;
                    break;
                }
                else
                    isValid = true;

            }
            Console.Write($"Валидация введеного массива     :");

            return isValid;
        }
    }


    internal class Program
    {
        static void Menu()
        {
            Console.WriteLine($"\nTask 3 \nДана строковая последовательность." +
                $"\nСтроки последовательности содержат только заглавные буквы латинского алфавита." +
                $"\nОтсортировать последовательность по возрастанию длин строк, а строки одинаковой длины – по убыванию.\n\n");
            Console.WriteLine("\t\tМеню");
            Console.WriteLine("1. Использовать заранее подготовленный массив целых чисел");
            Console.WriteLine("2. Ввести числа в массив вручную");
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

                        string[] array = { "ABCD", "ABC", "AB", "WXYZ", "WXY", "WX", "EFGH", "EFG", "EF" };
                        WorkWithStringArray workArray = new WorkWithStringArray(array);

                        Console.Write("\nМассив для примера              : ");
                        workArray.Show();
                        Console.WriteLine();

                        workArray.WorkWithArray();

                        Console.Write("Отсортированный массив          : ");
                        workArray.Show();

                        break;


                    case 2:

                        WorkWithStringArray userArray = new WorkWithStringArray();
                        Console.Write("\nВаш массив            : ");
                        userArray.Show();
                        Console.WriteLine();

                        userArray.WorkWithArray();

                        Console.Write("Отсортированный массив: ");
                        userArray.Show();

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
