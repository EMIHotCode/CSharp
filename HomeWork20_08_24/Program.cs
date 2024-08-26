using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace Programm
{
    public class WorkWithTxtFile
    {
        private string name;
        private string surname;    //фамилия 
        private string patronymic; //отчество
        public DateTime MyBirthday;
        public DateTime NowDate;
        public double[,] MassivDouble;
        public int[,] MassivInt;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get ; set ; }

        public WorkWithTxtFile() 
        {
            Name = "Михаил";
            Surname = "Ефремов";
            Patronymic = "Иванович";
            MyBirthday = new DateTime(1984, 12, 3);
            MassivDouble = new double [,] 
            {
                { 1.23, 2.25, 3.3}, { 1.1324, 2.23, 3.78}, { 4.1, 5.2, 3}, { 4.05, 5.012210, 3.05}
            };
            MassivInt = new int[,]
            {
                { 1, 2, 3, 4}, { 5, 6, 7, 8}, { 9, 10, 11, 12}
            };
            NowDate = DateTime.Now;
        }

        public WorkWithTxtFile(string _name, string _surname, string _patronymic, DateTime _bitthday, double[,] _doubleArr, int[,] _intArr, DateTime _now)
        {
            Name = _name;
            Surname = _surname;
            Patronymic = _patronymic;
            MyBirthday = _bitthday;
            MassivDouble = _doubleArr;
            MassivInt = _intArr;
            NowDate = _now;
        }

        public void Show()
        {
            Console.WriteLine($"\nИмя - {Name} Фамилия - {Surname} Отчество - {Patronymic} ");
            Console.WriteLine($"Дата рождения(чч.мм.гг) - {MyBirthday.Day}.{MyBirthday.Month}.{MyBirthday.Year}");
            Console.WriteLine("\nМассив дробных чисел:");

            for (int i = 0; i < MassivDouble.GetLength(0); i++)
            {
                for (int j = 0; j < MassivDouble.GetLength(1); j++)
                {
                    Console.Write($"{MassivDouble[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nМассив целых чисел:");

            for (int i = 0; i < MassivInt.GetLength(0); i++)
            {
                for (int j = 0; j < MassivInt.GetLength(1); j++)
                {
                    Console.Write($"{MassivInt[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\nДата сегодня - {NowDate.Day}.{NowDate.Month}.{NowDate.Year}");
        }

    }
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

            string fileName, filePath = ".\\", str;

            WorkWithTxtFile callObj = new WorkWithTxtFile();
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
                        str = string.Empty;

                        if (filePath == ".\\")
                        {
                            Console.WriteLine("\nСоздайте файл Пункт Меню 1 преджде чем записывать в него информацию");
                            break;
                        }
                        Console.WriteLine("\nИМЕЕМ ДАННЫЕ ОБЪЕКТА КЛАССА которые нужно записать в файл");
                        callObj.Show();
                        Console.WriteLine("\nЗАПИСЬ В ФАЙЛ НАЧАТА");

                        using ( FileStream fileStream = new FileStream(filePath, FileMode.Create)) 
                        {
                            using (StreamWriter writer = new StreamWriter(fileStream, Encoding.Unicode)) 
                            {
                                Console.WriteLine("\nЗапись в файл ФИО и DateTime{дата рождения} -> Ефремов Михаил Иванович 12.03.1984");
                                str = callObj.Name + " " + callObj.Surname + " " + callObj.Patronymic + " " + callObj.MyBirthday.Day.ToString() + " " + callObj.MyBirthday.Month.ToString() + " " + callObj.MyBirthday.Year.ToString();
                                writer.WriteLine(str);
                                Thread.Sleep(1000);
                                str = String.Empty;

                                Console.WriteLine($"\nЗапись в файл число строк и столбцов массива дробных чисел -> {callObj.MassivDouble.GetLength(0)} {callObj.MassivDouble.GetLength(1)}");
                                str = callObj.MassivDouble.GetLength(0).ToString() + " " + callObj.MassivDouble.GetLength(1).ToString();
                                writer.WriteLine(str);
                                Thread.Sleep(1000);


                                Console.WriteLine("\nЗапись в файл массива дробных чисел построчно ->");
                                str = String.Empty;

                                for (int i = 0; i < callObj.MassivDouble.GetLength(0); i++)
                                {
                                    for (int j = 0; j < callObj.MassivDouble.GetLength(1); j++)
                                    {
                                        str += (callObj.MassivDouble[i, j] + " ");
                                    }
                                    Console.WriteLine($"Строка {i+1} -> {str}");
                                    writer.WriteLine(str);
                                    Thread.Sleep(700);
                                    str = String.Empty;
                                }

                                Console.WriteLine($"\nЗапись в файл число строк и столбцов массива целых чисел -> {callObj.MassivInt.GetLength(0)} {callObj.MassivInt.GetLength(1)}");
                                str = callObj.MassivInt.GetLength(0).ToString() + " " + callObj.MassivInt.GetLength(1).ToString();
                                writer.WriteLine(str);
                                Thread.Sleep(1000);


                                Console.WriteLine("\nЗапись в файл массива целых чисел в одну строку ->");
                                str = String.Empty;

                                for (int i = 0; i < callObj.MassivInt.GetLength(0); i++)
                                {
                                    for (int j = 0; j < callObj.MassivInt.GetLength(1); j++)
                                    {
                                        str += (callObj.MassivInt[i, j] + " ");
                                    }
                                }
                                Console.WriteLine($"Результат -> {str}");
                                writer.WriteLine(str);
                                Thread.Sleep(1000);

                                str = String.Empty;
                                Console.WriteLine($"\nЗапись в файл DateTime(текущей даты) -> {callObj.NowDate.Day} {callObj.NowDate.Month} {callObj.NowDate.Year}\n");
                                str = callObj.NowDate.Day.ToString() + " " + callObj.NowDate.Month.ToString() + " " + callObj.NowDate.Year.ToString();
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
                                    str = String.Empty;

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
                        string _name, _surname, _patronymic;
                        DateTime _MyBirthday, _NowDate;
                        double[,] doubleMassiv;
                        int[,] intMassiv;


                        if (filePath != ".\\")
                        {
                            FileInfo fileInf = new FileInfo(filePath);
                            if (fileInf.Exists)
                            {
                                using (StreamReader reader = new StreamReader(filePath, Encoding.Unicode))
                                {
                                    // восстановление из 1 строки
                                    str = reader.ReadLine(); 
                                    string[] textArr = str.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    _name = textArr[0].ToString();
                                    _surname = textArr[1];
                                    _patronymic = textArr[2];
                                    _MyBirthday = new DateTime(Convert.ToInt32(textArr[5]), Convert.ToInt32(textArr[4]), Convert.ToInt32(textArr[3]));

                                    // восстановление из 2 строки
                                    str = reader.ReadLine(); 
                                    textArr = str.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    doubleMassiv = new double[Convert.ToInt32(textArr[0]), Convert.ToInt32(textArr[1])];

                                    // восстановление из 3, 4, 5, 6 строки
                                    for (int i = 0; i < doubleMassiv.GetLength(0); i++)
                                    {
                                        str = reader.ReadLine();
                                        textArr = str.Split(" \n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                        for (int j = 0; j < doubleMassiv.GetLength(1); j++)
                                        {
                                            doubleMassiv[i, j] = Convert.ToDouble(textArr[j]);
                                        }
                                    }

                                    // восстановление строк массива целых чисел 7, 8
                                    str = reader.ReadLine(); 
                                    textArr = str.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    intMassiv = new int[Convert.ToInt32(textArr[0]), Convert.ToInt32(textArr[1])];

                                    // восстановление массива целых чисел 
                                    str = reader.ReadLine();
                                    textArr = str.Split(" \n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 0; i < intMassiv.GetLength(0); i++)
                                    {
                                        for (int j = 0, k = (i * 4); j < 4; j++, k++)
                                        {
                                            intMassiv[i, j] = Convert.ToInt32(textArr[k]);
                                        }
                                    }

                                    // восстановление из последней строки строки
                                    str = reader.ReadLine(); 
                                    textArr = str.Split(" ,.:\t*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    _NowDate = new DateTime(Convert.ToInt32(textArr[2]), Convert.ToInt32(textArr[1]), Convert.ToInt32(textArr[0]));
                                    WorkWithTxtFile tempObj = new WorkWithTxtFile(_name, _surname, _patronymic, _MyBirthday, doubleMassiv, intMassiv, _NowDate);
                                    Console.WriteLine($"\nВсе данные из файла успешно восстановлены в объект \"tempObj\" класса\n");
                                    tempObj.Show();
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
