using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // обозреватель решений/ссылки/добавить System.ComponentModel.DataAnnotations
using System.Security.Cryptography;
using System.Xml.Linq;

namespace HomeWork6_2
{
    public class Shop
    {
        
        private string shopName { get; set; } // имя магазина 

        private string shopAdress { get; set; } // адрес магазина 

        private string shopSpecialization { get; set; } // описание профиля магазина 

        private string shopTelefon { get; set; } // контактный телефон магазина 

        private string shopEmail { get; set; } // контактный e-mail магазина 

        private double shopArea { get; set; } // площадь магазина 

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название магазина должно составлять от 3 до 50 символов")]
        public string ShopName
        {
            get { return shopName; }
            set 
            {
                shopName = value;
            }
        }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Адрес магазина должно составлять от 3 до 100 символов")]
        public string ShopAdress
        {
            get { return shopAdress; }
            set 
            {
                shopAdress = value;
            }
        }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Поле профиль магазина должно составлять от 3 до 50 символов")]
        public string ShopSpecialization
        {
            get { return shopSpecialization; }
            set
            {
                shopSpecialization = value;
            }
        }
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Поле телефон магазина должно составлять от 3 до 25 символов")]
        public string ShopTelefon
        {
            get { return shopTelefon; }
            set
            {
                shopTelefon = value;
            }
        }
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Поле E-mail магазина должно составлять от 6 до 50 символов")]
        public string ShopEmail
        {
            get { return shopEmail; }
            set
            {
                shopEmail = value;
            }
        }
        [Required]
        [Range(1, 300000, ErrorMessage = "Площадь должна быть от 1 до 300000.")]
        public double ShopArea
        {
            get { return shopArea; }
            set
            {
                shopArea = value;
            }
        }
        public Shop()
        {
            ShopName = string.Empty;
            ShopAdress = string.Empty;
            ShopSpecialization = string.Empty;
            ShopTelefon = string.Empty;
            ShopEmail = string.Empty;
            ShopArea = 0;
        }
        public Shop(string _name, string _adress, string _specialization, string _telefon, string _email, double _area)
        {

            ShopName = _name;
            ShopAdress = _adress;
            ShopSpecialization = _specialization;
            ShopTelefon = _telefon;
            ShopEmail = _email;
            ShopArea = _area;
        }

        public Shop initialNewShop() // инициализация нового магазина со всеми параметрами
        {
            double _area;
            Console.WriteLine("\nВвод данных о магазине");
            Console.Write("\nВведите название магазина:..........");
            ShopName = Console.ReadLine();
            Console.Write("\nВведите адрес магазина:.............");
            ShopAdress = Console.ReadLine();
            Console.Write("\nВведите описание профиля магазина:..");
            ShopSpecialization = Console.ReadLine();
            Console.Write("\nВведите контактный телефон магазина:");
            ShopTelefon = Console.ReadLine();
            Console.Write("\nВведите e-mail магазина:............");
            ShopEmail = Console.ReadLine();
            Console.Write("Введите площадь магазина:............");
            Double.TryParse(Console.ReadLine(), out _area);
            ShopArea = _area;
            Shop newShop = new Shop(ShopName, ShopAdress, ShopSpecialization, ShopTelefon, ShopEmail, ShopArea);

            return newShop ;
        }
        public void ShowShop() // вывод в консоль нового магазина со всеми параметрами
        {

            Console.Write("\nВывод данных о магазине:");
            Console.Write($"\nНазвание магазина:            {ShopName}");
            Console.Write($"\nАдрес магазина:               {ShopAdress}");
            Console.Write($"\nПрофиль магазина:             {ShopSpecialization}");
            Console.Write($"\nКонтактный телефон магазина:  {ShopTelefon}");
            Console.Write($"\nЕ-mail магазина:              {ShopEmail}");
            Console.Write($"\nПлощадь магазина:             {ShopArea}\n");
        }

        public static Shop operator +(Shop shop1, int n) // прибавление к объекту числа
        {
            shop1.ShopArea += n;
            return shop1;
        }

        public static Shop operator -(Shop shop1, int n) // вычитание числа из площади объекта
        {
            shop1.ShopArea -= n;
            return shop1;
        }
        public static bool operator ==(Shop shop1, Shop shop2)
        {
            return shop1.Equals(shop2);
        }
        public override bool Equals(object obj)
        {
            return this.ShopArea == ((Shop)obj).ShopArea;
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator !=(Shop shop1, Shop shop2)
        {
            return !shop1.Equals(shop2);
        }

        public static bool operator >(Shop shop1, Shop shop2)
        {
            return shop1.ShopArea > shop2.ShopArea;
        }

        public static bool operator <(Shop shop1, Shop shop2)
        {
            return shop1.ShopArea < shop2.ShopArea;
        }

    }
    internal class Program
    {
        static void Menu ()
        {
            Console.WriteLine($"\nКласс \"Магазин\". Добавлена информация о площади магазина. Выполнена перегрузка" +
                $"\"+\" \"-\" \"==\" \"< и >\" \"!= и Equals\"\n\n");
            Console.WriteLine("\t\tМеню");
            Console.WriteLine("1. Завести новый магазин");
            Console.WriteLine("2. Пройти тестовую функцию валидации");
            Console.WriteLine("3. Увеличить или уменьшить площадь магазина на указанную величину");
            Console.WriteLine("4. Сравнить площадь магазина с числом(проверка больше, меньше, равно)");
            Console.WriteLine("5. ВЫХОД");
        }
        static void Test_validationObj()
        {
            Shop shop1 = new Shop("OBI", "Мега, Федяково", "садовый", "+7 987 22-23-23", "obi@dtn.ru", 150); //true
            shop1.ShowShop();
            bool test1 = validation(shop1);
            Shop shop2 = new Shop("OB", "Мега, Федяково", "садовый", "+7 987 22-23-23", "obi@dtn.ru", 21);   // false мало символов в название магазина
            shop2.ShowShop();
            bool test2 = validation(shop2);
            Shop shop3 = new Shop("EuroSpar", "Гагарина 22", "подуктовый", "+7 987 22-23-23", "obi@dtn.ru", -100);  // false мала площадь
            shop3.ShowShop();
            bool test3 = validation(shop3);
            Shop shop4 = new Shop("Вкусно и точка", "Мега, Федяково", "садовый", "+7 987 22-23-23", "obi@dtn.ru", 300001); // false площадь больше Max
            shop4.ShowShop();
            bool test4 = validation(shop4);
            Shop shop5 = new Shop("Ситилинк", "Мега, Федяково", "компьютерный", "+", "obi@dtn.ru", 300000); // false телефон слишком короткий
            shop5.ShowShop();
            bool test5 = validation(shop5);
            if (test1 && !test2 && !test3 && !test4 && !test5)
                Console.WriteLine($"\nРезультат тестовой функции валидации: {true}");
            else
                Console.WriteLine($"\nРезультат тестовой функции валидации: {false}");
        }
        static bool validation(Shop newShop)
        {
            /* Валидация */
            var context = new ValidationContext(newShop);
            var results = new List<ValidationResult>();
            Console.WriteLine();

            if (!Validator.TryValidateObject(newShop, context, results, true))
            {
                Console.WriteLine($"Не удалось создать объект: {newShop.ShopName}");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return false;
            }
            else
            {
                Console.WriteLine($"Объект Shop успешно создан. Название: {newShop.ShopName}\n");
                return true;

            }
        }

        static void Main(string[] args)

        {
            bool exit = false;
            int choise, initialObjFlag = 0;
            Shop shop = new Shop();

            while (!exit)
            {

                Menu();

                Console.Write("Ваш выбор: ");
                Int32.TryParse(Console.ReadLine(), out choise);
                switch (choise)
                {
                    case 1:
                        while(true)
                        {
                            while (true)
                            {
                                Console.Write("\n1 - воспользоваться готовым объектом, 2 - ручной ввод данных о магазине: ");

                                if (Int32.TryParse(Console.ReadLine(), out choise))
                                    break;
                                else
                                {
                                    Console.WriteLine("ОШИБКА. Выберите чило 1 или 2");
                                    continue;
                                }
                            }

                            if (choise == 1)
                            {
                                shop = new Shop("OBI", "Мега, Федяково", "садовый", "+7 987 22-23-23", "obi@dtn.ru", 1500);
                                validation(shop);
                                shop.ShowShop();
                                initialObjFlag = 1;
                                break;
                            }
                            else
                            {
                                shop = shop.initialNewShop();
                                if (validation(shop) == true)
                                {
                                    shop.ShowShop();
                                    initialObjFlag = 1;
                                    break;
                                }
                                else
                                    continue;
                            }

                        }

                        break;
                        

                    case 2:
                        Test_validationObj();
                        break;
                    case 3:
                        if(initialObjFlag == 0)
                        {
                            Console.Write("\n Сначала создайте объект магазина Пункт 1 МЕНЮ: ");
                            break;
                        }
                        shop.ShowShop();

                        double newArea;
                        while (true)
                        {
                            Console.Write("\nУкажите величину на которую изменить площадь магазина созданного в (пункте 1 МЕНЮ): ");


                            if (Double.TryParse(Console.ReadLine(), out newArea))
                            {
                                shop.ShopArea += newArea;
                                if (shop.ShopArea > 0 && shop.ShopArea <=300000)
                                    break;
                                else
                                {
                                    Console.Write("\nИтоговая площадь магазина выходит за рамки допустимых значений от 1 до 300000.");
                                    shop.ShopArea -= newArea;    
                                    continue;
                                }
                            }

                            else
                            {
                                Console.WriteLine("ОШИБКА. Введите число от 1 до 300000");
                                continue;
                            }
                        }
                        shop.ShowShop();

                        break;
                    case 4:

                        if (initialObjFlag == 0)
                        {
                            Console.Write("\n Сначала создайте объект магазина Пункт 1 МЕНЮ: ");
                            break;
                        }
                        double number;
                        Shop temp_shop;
                        shop.ShowShop();

                        while (true)
                        {
                            Console.Write($"\nВведите площадь (от 1 до 300000) для сравнения с площадью магазина {shop.ShopName}: ");


                            if (Double.TryParse(Console.ReadLine(), out number))
                            {
                                temp_shop = new Shop("temp_shop", "temp_adress", "temp_specialization", "temp_telephon", "temp_email", number);
                                if (validation(temp_shop) == true)
                                    break;
                                else
                                    continue;
                            }

                            else
                            {
                                Console.WriteLine("ОШИБКА. Введите число от 1 до 300000");
                                continue;
                            }
                        }


                        if (shop > temp_shop)
                        {
                            Console.WriteLine($"{shop.ShopName} Площадь: {shop.ShopArea} - больше площади для сравнения: {temp_shop.ShopArea}");
                        }
                        else if (shop < temp_shop)
                        {
                            Console.WriteLine($"{shop.ShopName} Площадь: {shop.ShopArea} -  меньше площади для сравнения: {temp_shop.ShopArea}");

                        }
                        else if (shop == temp_shop)
                        {
                            Console.WriteLine($"Площади равны");
                        }

                        break;
                    case 5:

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