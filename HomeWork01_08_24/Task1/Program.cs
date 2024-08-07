using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations; // обозреватель решений -> ссылки -> добавить System.ComponentModel.DataAnnotations

namespace Programm
{
    public class WorkWithIntArray
    {
        [Required]
        [Range(-100000000, 100000000, ErrorMessage = "Числа в массиве должны быть от -100000000 до 100000000.")]
        public int[] Array;

        public WorkWithIntArray()
        {
            InitialIntArray();
        }
        public WorkWithIntArray(int[] array)
        {
            Array = array;
        }

        public void DistinctNumbers()  //отбора уникальных записей в массиве
        {
            IEnumerable<int> arreyForDistinct = from i in Array
                                                where i % 2 != 0
                                                select i;
            int[] result = arreyForDistinct.Take(1).Concat(arreyForDistinct.Skip(1).Distinct()).ToArray();
            Array = result;
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
            bool exit = false, exitForeach = false;
            while (!exit)
            {
                Console.Write("Вводите числа для создания массива через ПРОБЕЛ: ");

                string foo = Console.ReadLine();
                string[] tokens = foo.Split(' ');
                Array = new int[tokens.Length];
                int oneNum, iteration = 0;

                foreach (string s in tokens)
                {
                    if (Int32.TryParse(s, out oneNum) && (oneNum > -100000000 && oneNum < 100000000))
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"\rМассив должен состоять из целых чисел от -100000000 до 100000000");
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
    }
    internal class Program
    {
        static void Menu()
        {
            Console.WriteLine($"\nTask 1 \nДана целочисленная последовательность. " +
                $"Извлечь из нее все нечетные числа, сохранив \nих исходный порядок" +
                $" следования и удалив все вхождения повторяющихся элементов, кроме первых.\n\n");
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
                        
                        int[] array = { 2, 7, 88, -10, 15, 20, 12, -3, 0, 62, 5, -11, 7, 15 };
                        WorkWithIntArray workArray = new WorkWithIntArray(array);

                        Console.Write("\nМассив для примера       : ");
                        workArray.Show();
                        workArray.DistinctNumbers();

                        Console.Write("\nРезультат после изменений: ");
                        workArray.Show();

                        break;


                    case 2:

                        WorkWithIntArray userArray = new WorkWithIntArray();
                        Console.Write("\nВаш массив целых чисел   : ");
                        userArray.Show();

                        userArray.DistinctNumbers();

                        Console.Write("\nРезультат после изменений: ");
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
