using System;

namespace HomeWork8
{
    public class Book
    {
        private string author {  get; set; } // автор книги
        private string title { get; set; }   // название книги 
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public Book(){}
        public Book(string _author, string _title)
        {
            Author = _author;
            Title = _title;
        }

    }

    public class ListOfBooks
    {
        public Book[] booksArr;

        public ListOfBooks()
        {
            booksArr = new Book[]
            {
                new Book("Герберт Шилдт", "Полное руководство С# 4.0"),
                new Book("Стив Макконел", "Совершенный код"),
                new Book("Александр Шевчук", "Design Patterns via C#"),
                new Book("Сергей Тепляков", "Паттерны проектирования на платформе .NET")
            };
        }
        public int Lenght { get { return booksArr.Length; } }
        public Book this[int index]
        {
            get
            {
                if (index >= 0 && index < booksArr.Length)
                    return booksArr[index];
                throw new IndexOutOfRangeException();
            }
            set
            { booksArr[index] = value; }
        }

        public void ShowListOfBooks()
        {
            string bookVar;                // слово "книга" в зависимости от количества
            if (Lenght % 10 == 0 || Lenght % 10 >= 5 && Lenght % 10 <= 9 || Lenght >= 10 && Lenght % 10 <= 20)
                bookVar = "книг";
            else if (Lenght % 10 == 2 || Lenght % 10 == 3 || Lenght % 10 == 4)
                bookVar = "книги";
            else
                bookVar = "книга";
            Console.WriteLine($"\nВ наличии {Lenght} {bookVar}:");
            for (int i = 0; i < Lenght; i++)
            {
                Console.WriteLine($"{i + 1}. {booksArr[i].Author} - {booksArr[i].Title}");
            }

        }
        public void AddBookInArrey()
        {
            Console.Write("\nДанные новой книги:");

            int indexOfNewBook;
            Console.Write($"\nВведите номер от {1} до {Lenght+1} под которым будет лежать новая книга: ");
            while (true)   
            {
                if (Int32.TryParse(Console.ReadLine(), out indexOfNewBook))
                {
                    if (indexOfNewBook < 1 || indexOfNewBook > Lenght + 1)
                    {
                        Console.Write($"\nВведите номер от {1} до {Lenght + 1}");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.Write($"\nВведите число от {1} до {Lenght + 1}");
                    continue;
                }
            }
            Book newAddBook = new Book();
            Console.Write("\nВведите автора книги:  ");
            newAddBook.Author = Console.ReadLine().ToString();
            Console.Write("\nВведите название книги:");
            newAddBook.Title = Console.ReadLine().ToString();
            Array.Resize(ref booksArr, booksArr.Length + 1);
            booksArr[Lenght-1] = booksArr[indexOfNewBook - 1];
            booksArr[indexOfNewBook - 1] = newAddBook;
        }
        public void DellBookInArrey()
        {
            int indexOfDellBook;
            if (Lenght == 0)
            {
                Console.Write($"\nВ списке отсутствуют книги для удаления.");
                return;
            }
            Console.Write($"\nВведите номер книги от {1} до {Lenght} которую нужно удалить: ");
            while (true)   
            {
                if (Int32.TryParse(Console.ReadLine(), out indexOfDellBook))
                {
                    if (indexOfDellBook < 1 || indexOfDellBook > Lenght)
                    {
                        Console.Write($"\nВведите номер от {1} до {Lenght}");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.Write($"\nВведите число от {1} до {Lenght}");
                    continue;
                }
            }
            for (int i = indexOfDellBook - 1; i < Lenght - 1; i++)
            {
                booksArr[i] = booksArr[i+1];
            }
            Array.Resize(ref booksArr, booksArr.Length - 1);
        }
        public void FindBookInArrey()
        {
            Console.Write("\nВведите имя автора или часть названия книги для поиска:  ");
            string searchInArrey = Console.ReadLine();
            Console.WriteLine("\nНайденые книги:  ");
            int count = 0;
            for (int i = 0; i < Lenght; i++)
            {
                if (booksArr[i].Author.Contains(searchInArrey) || booksArr[i].Title.Contains(searchInArrey))
                {
                    Console.WriteLine($"{i + 1}. {booksArr[i].Author} - {booksArr[i].Title}");
                    count++;
                }
            }
            string bookVar;                // слово "книга" в зависимости от количества
            if (count % 10 == 0 || count % 10 >= 5 && count % 10 <= 9 || count >= 10 && count % 10 <= 20)
                bookVar = "книг";
            else if (count % 10 == 2 || count % 10 == 3 || count % 10 == 4)
                bookVar = "книги";
            else
                bookVar = "книга";
            Console.WriteLine($"\nНайдено: {count} {bookVar}");
        }
        public void ChangeBooksInArrey()
        {
            Console.Write("\nВведите номера книг для замены из местами в списке.");

            if (Lenght < 2)
            {
                Console.Write($"\nКниг в списке меньше двух. Смена мест невозможна. ");
                return;
            }
            int indexFirst, indexSecond;
            Console.Write($"\nВведите номер первой книги: ");
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out indexFirst))
                {
                    if (indexFirst < 1 || indexFirst > Lenght)
                    {
                        Console.Write($"\nВведите номер книги в списке от {1} до {Lenght}: ");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.Write($"\nВведите номер книги в списке от {1} до {Lenght}: ");
                    continue;
                }
            }
            Console.Write($"\nВведите номер второй книги: ");
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out indexSecond))
                {
                    if (indexSecond < 1 || indexSecond > Lenght)
                    {
                        Console.Write($"\nВведите номер книги в списке от {1} до {Lenght}: ");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.Write($"\nВведите номер книги в списке от {1} до {Lenght}: ");
                    continue;
                }
            }

            Book temp = booksArr[indexFirst-1];
            booksArr[indexFirst - 1] = booksArr[indexSecond - 1];
            booksArr[indexSecond - 1] = temp;
        }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
            ListOfBooks spisok = new ListOfBooks();

            bool exit = false;
            int choise;


            while (!exit)
            {
                Console.WriteLine($"\nПриложение \"Список книг для прочтения\". Приложение позволяет " +
                    $"добавлять книги в список, удалять книги из списка, проверять есть ли такая " +
                    $"книга в списке и др.\n\n");
                Console.WriteLine("\t\tМеню");
                Console.WriteLine("1. Вывести список книг.");
                Console.WriteLine("2. Добавить книгу в список.");
                Console.WriteLine("3. Удалить книгу из списка.");
                Console.WriteLine("4. Проверить есть ли такая книга в списке.");
                Console.WriteLine("5. Поменять две книги местами в списке.");
                Console.WriteLine("6. ВЫХОД");
                Console.Write("Ваш выбор: ");
                Int32.TryParse(Console.ReadLine(), out choise);

                switch (choise)
                {
                    case 1:
                        spisok.ShowListOfBooks();
                        break;
                    case 2:
                        spisok.ShowListOfBooks();
                        spisok.AddBookInArrey();
                        spisok.ShowListOfBooks();
                        break;
                    case 3:
                        spisok.ShowListOfBooks();
                        spisok.DellBookInArrey();
                        spisok.ShowListOfBooks();
                        break;
                    case 4:
                        spisok.ShowListOfBooks();
                        spisok.FindBookInArrey();
                        break;
                    case 5:
                        spisok.ShowListOfBooks();
                        spisok.ChangeBooksInArrey();
                        spisok.ShowListOfBooks();

                        break;
                    case 6:
                        return;

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
