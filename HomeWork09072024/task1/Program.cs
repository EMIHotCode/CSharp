using System;
using System.Text;

namespace HomeWork6_2
{
    namespace PsevdoText
    {
        class MyString
        {
            private int Glasnye { get; set; }
            private int Soglasnye { get; set; }
            private int MaxLengthWord { get; set; }

            static Random rnd = new Random();

            public MyString()
            {
                Glasnye = 0;
                Soglasnye = 0;
                MaxLengthWord = 0;

            }
            public MyString(int _glas, int _sogl, int _max)
            {
                Glasnye = _glas;
                Soglasnye = _sogl;
                MaxLengthWord = _max;
            }
            public string generateString()
            {
                int countWords = rnd.Next(5, 8);  // количество слов в предложениии

                int glas = Glasnye, sogl = Soglasnye, maxLen = MaxLengthWord;
                string word;
                char[] endSign = { '.', '.', '!', '?', '.', '.', '.', '.', '!', '?' }; // массив знаков заканчивающих предложение

                StringBuilder Predlogenie = new StringBuilder();
                while (glas != 0 && sogl != 0)
                {
                    for (int i = 0; i < countWords; i++)
                    {
                        if (glas == 0 && sogl == 0)
                            break;
                        maxLen = rnd.Next(6, 10);  // длинна слова в предложении
                        word = WordsInText.OneWord.GenWord(ref glas, ref sogl, ref maxLen).ToString(); // генерация нового слова для строки
                        if (i == 0)
                            word = char.ToUpper(word[0]) + word.Substring(1);
                        Predlogenie.Append(word);  // добавление слова к строке
                        if (i == countWords - 1)    // выбор знака окончания предложения из массива endSign
                            Predlogenie.Append($"{endSign[rnd.Next(0, 9)]}");
                        Predlogenie.Append(" "); // пробел после каждого слова в предложении
                    }
                }
                return Predlogenie.ToString();
            }
        }

    }


    namespace WordsInText
    {
        class OneWord
        {
            static Random rnd = new Random();

            public OneWord()
            {

            }
            public static string GenWord(ref int _numGlas, ref int _numSogl, ref int _maxLenWord)
            {
                int min;
                if (_maxLenWord < 8)
                    min = 3;
                else if (_maxLenWord < 15)
                    min = 2;
                else
                    min = 1;

                int newWordLength = rnd.Next(min, _maxLenWord);
                char[] glasnBukv = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' }; // 10
                char[] soglasnBukv = { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'х', 'ш', 'щ' }; // 22

                string outString = "";
                for (int i = 0; i < newWordLength;)
                {
                    if (_numGlas == 0 && _numSogl == 0)
                        break;

                    if (_numGlas != 0 && i < newWordLength)
                    {
                        outString += glasnBukv[rnd.Next(0, 9)];
                        _numGlas--;
                        i++;
                    }
                    if (_numSogl != 0 && i < newWordLength)
                    {
                        outString += soglasnBukv[rnd.Next(0, 21)];
                        _numSogl--;
                        i++;
                    }
                }

                return outString;
            }
        }
    }
    internal class Program
    {
        public static bool Validation(int gl, int sogl, int maxLeng)
        {
            if (gl >= 20 && gl <= 1000)
                Console.Write("");
            else
                Console.WriteLine("Гласных должно быть от 20 до 1000");
            if (sogl >= 20 && sogl <= 1000)
                Console.Write("");
            else
                Console.WriteLine("согласных должно быть от 20 до 1000");
            if (maxLeng <= 20 && maxLeng >= 3)
                Console.Write("");
            else
                Console.WriteLine("Длина слова должна быть от 3 до 20 букв");

            if ((gl >= 20 && gl <= 1000) && (sogl >= 20 && sogl <= 1000) && (maxLeng <= 20 && maxLeng >= 3))
                return true;
            else
                return false;
        }
        static void Main(string[] args)
        {
            bool exit = false;
            int choise;


            while (!exit)
            {
                Console.WriteLine($"\nПрограмма генерирует псевдотекст на основе введеного количества гласных и согласных букв и максимальной длинны слова.\n\n");
                Console.WriteLine("\t\tМеню");
                Console.WriteLine("1. Сгенерировать текст.");
                Console.WriteLine("2. ВЫХОД");
                Console.Write("Ваш выбор: ");
                Int32.TryParse(Console.ReadLine(), out choise);



                switch (choise)
                {
                    case 1:
                        int glasn, soglasn, maxLenWord;
                        while (true)   // проверка на ввод целого числа
                        {
                            Console.Write("\nВведите количество гласных: ");

                            if (Int32.TryParse(Console.ReadLine(), out glasn))
                                break;
                            else
                            {
                                Console.WriteLine("ОШИБКА. Введите положительное целое число");
                                continue;
                            }
                        }

                        while (true)   // ввод количества согласных букв
                        {
                            Console.Write("\nВведите количество согласных: ");

                            if (Int32.TryParse(Console.ReadLine(), out soglasn))
                                break;
                            else
                            {
                                Console.WriteLine("ОШИБКА. Введите положительное целое число");
                                continue;
                            }
                        }

                        while (true)   // ввод количества согласных букв
                        {
                            Console.Write("\nВведите максимальную длинну слова: ");

                            if (Int32.TryParse(Console.ReadLine(), out maxLenWord))
                                break;
                            else
                            {
                                Console.WriteLine("ОШИБКА. Введите положительное целое число");
                                continue;
                            }
                        }

                        bool valid = Validation(glasn, soglasn, maxLenWord);
                        Console.WriteLine($"\nВалидация: {(bool)valid}");
                        PsevdoText.MyString newS = new PsevdoText.MyString(glasn, soglasn, maxLenWord);
                        string newStr = newS.generateString();
                        Console.WriteLine("\nРезультат работы программы: ");

                        Console.WriteLine($"{newStr} ");
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