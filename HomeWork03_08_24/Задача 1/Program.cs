using System;
using System.Linq;
using System.Collections.Generic;

namespace Programm
{
    public class WorkWithIntArray
    {
        public int[] Array;

        public WorkWithIntArray()
        {
            InitialIntArray();
        }
        public WorkWithIntArray(int[] array)
        {
            Array = array;
        }

        public void WorkWithArray(int[] array2)
        {
            var workWithArray = from i in Array
                                join j in array2 on i%10 equals j%10
                                select string.Format("{0}-{1}\n", i, j);
            Console.WriteLine("\nРезультат ключ(массив 1) - значение(массив 2) : ");
            foreach (var item in workWithArray)
            {
                Console.Write(item);

            }
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

        public void InitialIntArray()
        {
            bool exit = false;
            while (!exit)
            {
                bool exitForeach = false;
                Console.Write("Вводите числа для создания массива через ПРОБЕЛ: ");

                string foo = Console.ReadLine().Trim();
                string[] tokens = foo.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Array = new int[tokens.Length];
                int oneNum, iteration = 0;

                foreach (string s in tokens)
                {
                    if (Int32.TryParse(s, out oneNum) && (oneNum > 0))
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"\rМассив должен состоять из положительных целых чисел > 0");
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
                    if (Int32.TryParse(s, out oneNum))
                    {
                        Array[iteration] = oneNum;
                        iteration++;
                        exit = true;
                    }
                }
            }
        }

        public static bool isValidArray(int[] Array)
        {
            bool isValid = true;
            foreach (int i in Array)
            {
                if (i < 0 && i > 100000000)
                {
                    isValid = false;
                    break;
                }
                else
                    isValid = true;

            }
            Console.Write($"Валидация введенного массива           :");

            return isValid;
        }
    }
    internal class Program
    {
        static void Menu()
        {
            Console.WriteLine($"\nЗадача 1 \n\n");
            Console.WriteLine("\t\tМеню");
            Console.WriteLine("1. Использовать заранее подготовленные массивы целых неповторяющихся чисел");
            Console.WriteLine("2. Ввести числа в массивы вручную");
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

                        int[] array = { 1, 43, 275 };
                        int[] array2 = { 1, 14, 13, 21, 5, 2, 10, 73, 555, 80 };

                        WorkWithIntArray workArray = new WorkWithIntArray(array);
                        WorkWithIntArray workArray2 = new WorkWithIntArray(array2);

                        Console.Write("\nПервый массив для примера              : ");
                        workArray.Show();
                        Console.WriteLine();
                        Console.Write($" {WorkWithIntArray.isValidArray(array)}");

                        Console.WriteLine();
                        Console.Write("\nВторой мссив для примера               : ");
                        workArray2.Show();
                        Console.WriteLine();
                        Console.WriteLine($" {WorkWithIntArray.isValidArray(array2)}");

                        workArray.WorkWithArray(workArray2.Array);

                        break;


                    case 2:

                        Console.WriteLine("\nПервый массив целых чисел.");
                        WorkWithIntArray userArray1 = new WorkWithIntArray();
                        Console.Write("\nВаш первый массив целых чисел          :");
                        userArray1.Show();

                        Console.WriteLine();
                        Console.Write($" {WorkWithIntArray.isValidArray(userArray1.Array)}");

                        Console.WriteLine();
                        Console.WriteLine("\nВторой массив целых чисел.");
                        WorkWithIntArray userArray2 = new WorkWithIntArray();
                        Console.Write("\nВаш первый массив целых чисел          :");
                        userArray2.Show();
                        Console.WriteLine();
                        Console.WriteLine($" {WorkWithIntArray.isValidArray(userArray2.Array)}");

                        userArray1.WorkWithArray(userArray2.Array);

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
