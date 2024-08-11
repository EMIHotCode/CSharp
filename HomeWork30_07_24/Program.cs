using System;

namespace dayOfTheWeek
{
    enum DaysString { Понедельник, Вторник, Среда, Четверг, Пятница, Суббота, Воскресенье }

    delegate string MyDelegate();
    internal class Program
    {
        static void Main(string[] args)
        {
            MyDelegate show;
            bool exit = false;
            int num = -1;

            Console.WriteLine("\nНажимайте ПРОБЕЛ для показа дней недели, ESC - Выход: ");
            while (!exit)
            {
                show = delegate ()  // анонимный метод
                {
                    DaysString days = (DaysString)(++num % 7);
                    string dayToday = days.ToString();
                    return dayToday;
                };

                Console.Write($"\n{num + 2} - й день: {show()}");

                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    exit = true;
            }

        }
    }
}