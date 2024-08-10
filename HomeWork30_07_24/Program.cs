using System;

namespace dayOfTheWeek
{
    delegate string MyDelegate();
    internal class Program
    {
        static void Main(string[] args)
        {
            MyDelegate show;
            bool exit = false;
            int num = -1;
            string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

            Console.WriteLine("\nНажимайте ПРОБЕЛ для показа дней недели, ESC - Выход: ");
            while (!exit)
            {
                show = delegate ()  // анонимный метод
                {
                    return days[++num % 7];
                };

                Console.Write($"\n{num + 2} - й день: {show()}");

                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    exit = true;
            }

        }
    }
}
