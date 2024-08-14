using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace StoragesProgect
{
    //Astarct class for basic storage object
    abstract class Storage
    {
        public Dictionary<string, float> sections;  //Section ratio to memory volume (Соотношение разделов к объему памяти)

        private string _name;       //name of storage
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _model;      //model of storage
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public float SpeedRead { get; set; }         // Скорость чтения   Mbyte in second

        public float SpeedWrite { get; set; }        // Скорость записи    Mbyte in second


        public float Volume { get; set; } // Gigabyte
        /// <summary>
        /// Get volume of storage's memory
        /// </summary>
        /// <returns></returns>
        abstract public float GetVolumeOfMemory();

        /// <summary>
        /// Get volume of FREE storage's memory
        /// </summary>
        /// <returns></returns>
        abstract public float GetFreeVolumeOfMemory(float informationVolume);

    }

    class Hdd : Storage
    {
        public float Speed { get; set; }            //Mbyte in second
        public Hdd()
        {
            sections = new Dictionary<string, float>();
            Name = "HDD Seagate";
            Model = "ST1000LM035";
            Volume = 500;
            SpeedRead = 140;
            SpeedWrite = 40;
        }
        public Hdd(string name, string model, float volume, float speedR, float speedW)
        {
            sections = new Dictionary<string, float>();
            Name = name;
            Model = model;
            Volume = volume;
            SpeedRead = speedR;
            SpeedWrite = speedW;
        }

        public override string ToString()
        {
            //TODO print elements of dictionary
            return $"Name: {Name} Model: {Model} Volume: {Volume} SpeedRead: {SpeedRead} SpeedWrite: {SpeedWrite}";
        }

        public override float GetFreeVolumeOfMemory(float informationVolume)
        {
            return this.GetVolumeOfMemory() - informationVolume;
        }

        public override float GetVolumeOfMemory()
        {
            return this.Volume;
        }
    }
    class Ssd : Storage
    {
        public string Connector { get; set; }       //Connection connector
        public string Country { get; set; }         //Produced country
        public Ssd()
        {
            Name = "Ssd ExeGate";
            Model = "A400TS60";
            Connector = "SATA";
            Volume = 100;
            SpeedRead = 350;
            SpeedWrite = 250;
            Country = "China";
        }
        public Ssd(string name, string model, float volume, float speedR, float speedW, string connector, string country)
        {
            Name = name;
            Model = model;
            Volume = volume;
            SpeedRead = speedR;
            SpeedWrite = speedW;
            Connector = connector;
            Country = country;
        }

        public override string ToString()
        {
            return $"Name: {Name} Model: {Model} Volume: {Volume} Connector: {Connector} " +
                $"SpeedRead: {SpeedRead} SpeedWrite: {SpeedWrite} Country: {Country}";
        }

        public override float GetFreeVolumeOfMemory(float informationVolume)
        {
            return this.GetVolumeOfMemory() - informationVolume;
        }

        public override float GetVolumeOfMemory()
        {
            return this.Volume;
        }
    }
    class Flash : Storage
    {
        public Flash()
        {
            Name = "Flash Mirex";
            Model = "Mirex LINE";
            Volume = 16;
            SpeedRead = 60;
            SpeedWrite = 25;
        }

        public Flash(string name, string model, float volume, float speedR, float speedW)
        {
            Name = name;
            Model = model;
            Volume = volume;
            SpeedRead = speedR;
            SpeedWrite = speedW;
        }

        public override string ToString()
        {
            return $"Name: {Name} Model: {Model} Volume: {Volume} SpeedRead: {SpeedRead} SpeedWrite: {SpeedWrite}";
        }

        public override float GetFreeVolumeOfMemory(float informationVolume)
        {
            return this.GetVolumeOfMemory() - informationVolume;
        }

        public override float GetVolumeOfMemory()
        {
            return this.Volume;
        }
    }
    enum DvdType { OneSides = 5, TwoSides = 9 }  //Type of DVD Gbyte
    class Dvd : Storage
    {
        public DvdType DvdType { get; set; }

        public Dvd()
        {
            Name = "DVD VS";
            Model = "DVD+R";
            Volume = 4.7F;
            SpeedRead = 16;
            SpeedWrite = 16;
            DvdType = DvdType.OneSides;
        }

        public override string ToString()
        {
            return $"Name: {Name} Model: {Model} Volume: {Volume} SpeedRead: {SpeedRead} " +
                   $"SpeedWrite: {SpeedWrite} DvdType: {DvdType}";
        }

        public Dvd(string name, string model, float volume, float speedRead, float speedWrite, DvdType dvdType)
        {
            Name = name;
            Model = model;
            Volume = volume;
            SpeedRead = speedRead;
            SpeedWrite = speedWrite;
            DvdType = dvdType;
            if (DvdType == DvdType.OneSides)
            {
                Volume = 4.7F;
            }
            else
                Volume = 9.4F;

        }
        public override float GetFreeVolumeOfMemory(float informationVolume)
        {
            return this.GetVolumeOfMemory() - informationVolume;
        }

        public override float GetVolumeOfMemory()
        {
            return this.Volume;
        }

    }
    interface ICopyFromStorage  // интерфейс копирования
    {
        void CopyFromStorage(Storage storageFrom, Storage storageTo, float info); // метод откуда 
        void CopyToStorage(Storage storage, float info); // метод куда
    }

    class StorageIEnumerator : IEnumerator<Storage>  // класс для перебора 
    {
        private Storage[] storage;

        private int position = -1;
        public int Lenght { get { return storage.Length; } }

        public Storage Current { get { return storage[position]; } }

        public StorageIEnumerator(Storage[] newStorage)
        {
            storage = newStorage;
        }

        public void Reset()
        {
            position = -1;
        }

        public bool MoveNext()
        {
            position++;
            return position < storage.Length;
        }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    class CalculateStorages : IEnumerable, ICopyFromStorage
    {
        public Storage[] storages;
        public CalculateStorages(int size)
        {
            storages = new Storage[size];
        }

        public IEnumerator GetEnumerator()
        {
            return new StorageIEnumerator(storages);
        }
        public void CopyFromStorage(Storage storageFrom, Storage storageTo, float info)
        {
            storageFrom.Volume = storageFrom.GetFreeVolumeOfMemory(info) + info;
            storageTo.Volume = storageTo.GetFreeVolumeOfMemory(info);
        }

        public void CopyToStorage(Storage storage, float info)  // оставшейся информации 
        {
            storage.Volume = storage.GetFreeVolumeOfMemory(info);
        }

        public float CalculateTime(Storage storageFrom, Storage storageTo)
        {
            float time, speed;
            if (storageFrom.SpeedRead > storageTo.SpeedWrite)
                speed = storageTo.SpeedWrite;
            else
                speed = storageFrom.SpeedRead;

            time = storageFrom.Volume / speed;
            return time;
        }
        public double CalculateStoragesMemory()
        {
            double result = 0;

            foreach (var item in storages)
            {
                result += item.Volume;
            }
            return result;
        }
        // TODO метод расчет необходимого количества носителей информации представленных типов для переноса информации		
        public float NumberStorages(Storage storageFrom, Storage storageTo)
        {
            int number;
            number = (int)(storageFrom.Volume / storageTo.Volume) + 1;
            return number;
        }
    }

    internal class TestClass
    {
        CalculateStorages testStorage = new CalculateStorages(1);

        Storage testHdd = new Hdd("HDD test", "Baracuda", 500, 210, 120);
        Storage testSsd = new Ssd("Ssd test", "Kingston", 300, 450, 400, "M.2", "China");
        Storage testFlash = new Flash("Flash test", "Samsung", 120, 300, 300);
        Storage testDVD = new Dvd("DVD test", "Verbatime", 5, 50, 30, DvdType.OneSides);

        public void TestNumberStorages()
        {
            int result;
            string itog;

            result = (int)testStorage.NumberStorages(testHdd, testSsd);
            itog = (result == 2) ? "successfully" : "failed";
            Console.WriteLine($"Test запись HDD 500Gb на SSD 300Gb = 2шт - {itog}");


            result = (int)testStorage.NumberStorages(testSsd, testFlash);
            itog = (result == 3) ? "successfully" : "failed";
            Console.WriteLine($"Test запись SSD 300Gb на Flash 120Gb = 3шт - {itog}");

            result = (int)testStorage.NumberStorages(testFlash, testDVD);
            itog = (result == 26) ? "successfully" : "failed";
            Console.WriteLine($"Test запись Flash 120Gb на DVD 4,7Gb = 26шт - {itog}");

        }

        public void TestCalculateTime()
        {
            float result;
            string itog;

            result = testStorage.CalculateTime(testHdd, testSsd);
            itog = (result > 0) ? "successfully" : "failed";
            Console.WriteLine($"Test время записи HDD на SSD > 0 - {itog}");

            result = testStorage.CalculateTime(testFlash, testSsd);
            itog = (result > 0) ? "successfully" : "failed";
            Console.WriteLine($"Test время записи Flash на SSD > 0 - {itog}");

            result = testStorage.CalculateTime(testSsd, testDVD);
            itog = (result > 0) ? "successfully" : "failed";
            Console.WriteLine($"Test время записи Ssd на DVD > 0 - {itog}");


        }
    }
    internal class Program
    {
        static int Check(int answer)
        {
            while (answer < 1 || answer > 5)
            {
                Console.WriteLine("Вы ввели неправильный выбор. Введите число от 1 до 3");
                answer = Convert.ToInt32(Console.ReadLine());
            }
            return answer;
        }
        static int Menu()
        {
            int answer;
            Console.WriteLine("\nКакое действие вы хотите совершить?");
            Console.WriteLine("1. Расчет общего количества памяти всех устройств.");
            Console.WriteLine("2. Копирование информации на устройства.");
            Console.WriteLine("3. Расчет времени необходимого для копирования.");
            Console.WriteLine("4. Расчет необходимого количества носителей иформации для переноса.");
            Console.WriteLine("5. Тестирование методов класса.");
            Console.WriteLine("6. Выход.");
            Console.Write("Ваш выбор - ");
            answer = Convert.ToInt32(Console.ReadLine());
            return Check(answer);
        }
        static void Main(string[] args)
        {

            do
            {
                Console.WriteLine("Рабочий компьютер");
                Storage WorkStantion = new Hdd("Segate WorkStantion", "ST1320TTW", 565, 75, 40);
                Console.WriteLine(WorkStantion.ToString());



                CalculateStorages HomeStantion = new CalculateStorages(4) { };
                HomeStantion.storages[0] = new Hdd();
                HomeStantion.storages[1] = new Ssd();
                HomeStantion.storages[2] = new Flash();
                HomeStantion.storages[3] = new Dvd();

                switch (Menu())
                {
                    case 1:
                        Console.WriteLine("Домашний компьютер:");

                        foreach (var item in HomeStantion.storages)
                        {
                            Console.WriteLine($"Носитель: {item.Name} Объем: {item.Volume}");
                        }
                        Console.WriteLine($"\nОбщее количество памяти всех устройств = {HomeStantion.CalculateStoragesMemory():F} \n" );
                        break;

                    case 2:

                        foreach (var item in HomeStantion.storages)
                        {
                            Console.WriteLine($"\nПеренос с Рабочего компьютера на Домашний {item.Name} Объемом - {item.Volume} Gb файлов по 780Mb:");

                            Console.WriteLine($"");
                            float tempVolume = item.Volume;
                            for (int i = 0; i < 16; i++)
                            {
                                if (item.Volume < 0.78)
                                {
                                    item.Volume = tempVolume;
                                    Console.WriteLine($"Берем новый носитель {item.Name} объемом {item.Volume:F} и начинаем копировать");
                                }
                                HomeStantion.CopyFromStorage(WorkStantion, item, (float)0.78);
                                Console.WriteLine($"Копирование - Носитель {item.Name} осталось места:{item.Volume:F}");

                            }
                            Console.WriteLine("так далее... Пример копирования информации закончен\n\n");

                        }

                        break;
                    case 3:
                        Console.WriteLine($"\nВремя переноса с Рабочего компьютера {WorkStantion.Name} на Домашний:");
                        foreach (var item in HomeStantion.storages)
                        {
                            Console.WriteLine($"Носитель: {item.Name} время переноса: {HomeStantion.CalculateTime(WorkStantion, item)} сек");
                        }
                        break;
                    case 4:
                        Console.WriteLine($"\nДля переноса с Рабочего компьютера {WorkStantion.Name} {WorkStantion.Volume} Gb на Домашний:");
                        foreach (var item in HomeStantion.storages)
                        {
                            Console.WriteLine($"Нужно носитель: {item.Name} oбъем: {item.Volume} - {HomeStantion.NumberStorages(WorkStantion, item)} штук");
                        }
                        break;
                    case 5:
                        Console.WriteLine($"\nТестирование метода определения требуемого количества внешних накопителей для переноса информации NumberStorages():");
                        TestClass test = new TestClass();
                        test.TestNumberStorages();
                        test.TestCalculateTime();
                        break;
                    case 6:
                        return;


                    default:
                        Console.Clear();
                        Console.WriteLine("\nВведено неверное значение.\n");
                        Console.ReadKey();
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            } while (true);

        }
    }
}
