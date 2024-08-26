using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace Programm
{
    internal class Program
    {
        static void Menu()
        {
            Console.WriteLine($"\nПрограмма чтения и записи в файл \n\n");
            Console.WriteLine("\t\tМеню");
            Console.WriteLine("1. Создать текстовый файл в текущей директории программы");
            Console.WriteLine("2. Записать форматированную информацию в файл");
            Console.WriteLine("3. Открыть и прочесть файл созданый в пункте 1 Меню");
            Console.WriteLine("4. Преобразовать данные из файла в переменные с учетом структуры");
            Console.WriteLine("5. ВЫХОД");
        }
        static void Main(string[] args)
        {

            bool exit = false;
            int choise;

            string fileName, filePath = ".\\", FIO;
            DateTime myBirthday = new DateTime(1984, 12, 3);

            double[,] massiv =
            {
                { 1.23, 2.25, 3.3},
                { 1.1324, 2.23, 3.78},
                { 4.1, 5.2, 3},
                { 4.05, 5.012210, 3.05}
            };

            int[,] massivInt =
            {
                { 1, 2, 3, 4},
                { 5, 6, 7, 8},
                { 9, 10, 11, 12}
            };


            while (!exit)
            {
                Menu();

                Console.Write("Ваш выбор: ");
                Int32.TryParse(Console.ReadLine(), out choise);
                switch (choise)
                {
                    case 1:

                        filePath = ".\\";

                        Regex regex = new Regex(@"^\w+\.txt$"); // шаблон принимающий имя файла с раширением txt
                        while (true)
                        {
                            Console.Write("Введите имя создаваемого файла c расширением txt: ");
                            fileName = Console.ReadLine().Trim();

                            if (regex.IsMatch(fileName))
                            {
                                filePath += fileName;
                                break;
                            }
                            else
                                Console.WriteLine("ОШИБКА. Введите правильно расширение .txt или не используйте символы в названии %;?:!*?*)(<>");
                        }

                        FileInfo fileInfo = new FileInfo(filePath);
                        if (fileInfo.Exists)
                        {
                            Console.WriteLine($"\nФайл: {fileInfo.Name} с таким именем уже существует в текущем каталоге");
                            Console.WriteLine($"Имя файла: {fileInfo.Name}");
                            Console.WriteLine($"Путь к файлу: {fileInfo.FullName}");
                            Console.WriteLine($"Время создания: {fileInfo.CreationTime}");
                            Console.WriteLine($"Размер: {fileInfo.Length}");
                        }
                        else
                        {
                            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                            {
                            Console.WriteLine($"\nФайл: {fileInfo.Name} создан.");
                            }
                        }
                    break;


                    case 2:
                        string str = string.Empty;

                        if (filePath == ".\\")
                        {
                            Console.WriteLine("\nСоздайте файл Пункт Меню 1 преджде чем записывать в него информацию");
                            break;
                        }
                        using ( FileStream fileStream = new FileStream(filePath, FileMode.Create)) 
                        {
                            using (StreamWriter writer = new StreamWriter(fileStream, Encoding.Unicode)) 
                            {
                                Console.WriteLine("\nЗапись в файл ФИО и DateTime{дата рождения} -> Ефремов Михаил Иванович 12.03.1984");
                                FIO = "Ефремов Михаил Иванович";
                                str = FIO + " " + myBirthday.Month.ToString() + " " + myBirthday.Day.ToString() + " " + myBirthday.Year.ToString();
                                writer.WriteLine(str);
                                Thread.Sleep(1000);

                                Console.WriteLine($"\nЗапись в файл число строк и столбцов массива дробных чисел -> {massiv.GetLength(0)} {massiv.GetLength(1)}");
                                str = massiv.GetLength(0).ToString() + " " + massiv.GetLength(1).ToString();
                                writer.WriteLine(str);
                                Thread.Sleep(1000);


                                Console.WriteLine("\nЗапись в файл массива дробных чисел построчно ->");
                                str = String.Empty;

                                for (int i = 0; i < massiv.GetLength(0); i++)
                                {
                                    for (int j = 0; j < massiv.GetLength(1); j++)
                                    {
                                        str += (massiv[i, j] + " ");
                                    }
                                    Console.WriteLine($"Строка {i+1} -> {str}");
                                    writer.WriteLine(str);
                                    Thread.Sleep(700);
                                    str = String.Empty;
                                }

                                Console.WriteLine($"\nЗапись в файл число строк и столбцов массива целых чисел -> {massivInt.GetLength(0)} {massivInt.GetLength(1)}");
                                str = massivInt.GetLength(0).ToString() + " " + massivInt.GetLength(1).ToString();
                                writer.WriteLine(str);
                                Thread.Sleep(1000);


                                Console.WriteLine("\nЗапись в файл массива целых чисел в одну строку ->");
                                str = String.Empty;

                                for (int i = 0; i < massivInt.GetLength(0); i++)
                                {
                                    for (int j = 0; j < massivInt.GetLength(1); j++)
                                    {
                                        str += (massivInt[i, j] + " ");
                                    }
                                }
                                Console.WriteLine($"Результат -> {str}");
                                writer.WriteLine(str);
                                Thread.Sleep(1000);
                                str = String.Empty;

                                DateTime nowDate = DateTime.Now;
                                str = String.Empty;
                                Console.WriteLine($"\nЗапись в файл DateTime(текущей даты) -> {nowDate.Day} {nowDate.Month} {nowDate.Year}\n");
                                str = nowDate.Day.ToString() + " " + nowDate.Month.ToString() + " " + nowDate.Year.ToString();
                                writer.WriteLine(str);
                                Thread.Sleep(1000);
                            }
                            FileInfo fileInf = new FileInfo(filePath);
                            if (fileInf.Exists)
                            {
                                Console.WriteLine($"Вся информация записана в файл в текущем каталоге: {fileInf.Name}");
                                Console.WriteLine($"Время создания: {fileInf.CreationTime}");
                                Console.WriteLine($"Размер файла: {fileInf.Length}");
                            }
                        }
                        break;
                    case 3:
                        if (filePath != ".\\")
                        {
                            FileInfo fileInf = new FileInfo(filePath);
                            if (fileInf.Exists)
                            {
                                using (StreamReader reader = new StreamReader(filePath, Encoding.Unicode))
                                {
                                    str = reader.ReadToEnd(); // чтение из файла

                                    Console.WriteLine($"{str}\n\nФайл успешно прочитан.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nТакого файла в текущем каталоге нет. Укажите другое имя файла пункт Меню 1");
                            }
                        }
                        else
                            Console.WriteLine("\nОШИБКА. Укажите имя файла пункт Меню 1 который будем выводить на экран");
                        break;
                    case 4:

                        if (filePath != ".\\")
                        {
                            FileInfo fileInf = new FileInfo(filePath);
                            if (fileInf.Exists)
                            {
                                using (StreamReader reader = new StreamReader(filePath, Encoding.Unicode))
                                {
                                    // восстановление из 1 строки
                                    str = reader.ReadLine(); // чтение из файла
                                    string[] textArr = str.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    FIO = textArr[0] + " " + textArr[1] + " " + textArr[2]; // Возвращаем имя фамилию и отчество в переменную FIO
                                    DateTime testBirthday = new DateTime(Convert.ToInt32(textArr[5]), Convert.ToInt32(textArr[3]), Convert.ToInt32(textArr[4]));
                                    Console.WriteLine($"\nФамилие Имя Отчество восстановлено в переменную =  {FIO}\n" +
                                        $"Дата рождения восстановлена из файла в DateTime = {testBirthday.Month} {testBirthday.Day} {testBirthday.Year}");

                                    // восстановление из 2 строки
                                    str = reader.ReadLine(); // чтение из файла
                                    textArr = str.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    massiv = new double[Convert.ToInt32(textArr[0]), Convert.ToInt32(textArr[1])];
                                    Console.WriteLine($"\nЧисло строк {Convert.ToInt32(textArr[0])} и стоблцов {Convert.ToInt32(textArr[1])}  массива дробных чисел восстановлено.");

                                    // восстановление из 3, 4, 5, 6 строки
                                    Console.WriteLine($"\nВывод массива дробных чисел восстановленный из файла:");

                                    for (int i = 0; i < massiv.GetLength(0); i++)
                                    {
                                        str = reader.ReadLine();
                                        textArr = str.Split(" \n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                        for (int j = 0; j < massiv.GetLength(1); j++)
                                        {
                                            massiv[i, j] = Convert.ToDouble(textArr[j]);
                                            Console.Write($"{massiv[i, j]} ");
                                        }
                                        Console.WriteLine();
                                    }

                                    // восстановление строк массива целых чисел 7, 8
                                    str = reader.ReadLine(); // чтение из файла
                                    textArr = str.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    massivInt = new int[Convert.ToInt32(textArr[0]), Convert.ToInt32(textArr[1])];
                                    Console.WriteLine($"\nЧисло строк {Convert.ToInt32(textArr[0])} и стоблцов {Convert.ToInt32(textArr[1])}  массива целых чисел восстановлено.");

                                    // восстановление массива целых чисел 
                                    Console.WriteLine($"\nВывод массива целых чисел восстановленный из файла:");
                                    str = reader.ReadLine();
                                    textArr = str.Split(" \n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 0; i < massivInt.GetLength(0); i++)
                                    {
                                        for (int j = 0, k = (i * 4); j < 4; j++, k++)
                                        {
                                            massivInt[i, j] = Convert.ToInt32(textArr[k]);
                                            Console.Write($"{massivInt[i, j]} ");
                                        }
                                        Console.WriteLine();
                                    }

                                    // восстановление из 1 строки
                                    str = reader.ReadLine(); // чтение из файла
                                    textArr = str.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    DateTime dateFromFile = new DateTime(Convert.ToInt32(textArr[2]), Convert.ToInt32(textArr[1]), Convert.ToInt32(textArr[0]));
                                    Console.WriteLine($"\nДата восстановлена из файла в DateTime Год - {dateFromFile.Year} Месяц - {dateFromFile.Month} День - {dateFromFile.Day} ");

                                }

                            }
                            else
                            {
                                Console.WriteLine("\nТакого файла в текущем каталоге нет. Укажите другое имя файла пункт Меню 1");
                            }
                        }
                        else
                           Console.WriteLine("\nОШИБКА. Укажите имя файла пункт Меню 1 который будем выводить на экран");

                    break;

                    case 5:
                        return;

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
