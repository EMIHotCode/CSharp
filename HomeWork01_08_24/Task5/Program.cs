using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations; // обозреватель решений -> ссылки -> добавить System.ComponentModel.DataAnnotations
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;

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

        public void WorkWihtArray()
        {
            Console.WriteLine($" {isValidArray(Array)}");

            IEnumerable<int> arreyForWork = from i in Array
                                            where i > 0
                                            select i;
            Array = arreyForWork.ToArray();
            Console.Write($"Положительные числа массива     : ");
            Show();
            Console.WriteLine();

            arreyForWork = from i in Array
                           select i % 10;
            Array = arreyForWork.ToArray();
            Console.Write($"Последние цифры чисел           : ");
            Show();

            int[] result = arreyForWork.Take(1).Concat(arreyForWork.Skip(1).Distinct()).ToArray();
            Array = result.ToArray();
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
        public static bool isValidArray(int[] Array)
        {
            bool isValid = true;
            foreach (int i in Array)
            {
                if (i < -100000000 && i > 100000000)
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
            Console.WriteLine($"\nTask 5 \nДана целочисленная последовательность.\nОбрабатывая только положительные числа, получить последовательность их последних цифр и удалить \nв полученной последовательности все вхождения одинаковых цифр, кроме первого.\nПорядок полученных цифр должен соответствовать порядку исходных чисел.\n\n");
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

                        int[] array = { 200, 100, 99, 20, 45, 9, 0, -1, -20, -100 };
                        WorkWithIntArray workArray = new WorkWithIntArray(array);

                        Console.Write("\nМассив для примера              : ");
                        workArray.Show();
                        Console.WriteLine();

                        workArray.WorkWihtArray();

                        Console.Write("\nУдалены одинаковые кроме первого: ");
                        workArray.Show();

                        break;


                    case 2:

                        WorkWithIntArray userArray = new WorkWithIntArray();
                        Console.Write("\nВаш массив целых чисел          : ");
                        userArray.Show();
                        Console.WriteLine();

                        userArray.WorkWihtArray();

                        Console.Write("\nРезультат после изменений       : ");
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
