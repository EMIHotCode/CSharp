using System.Collections.Generic;
using System;

class Team : IWorker
{
    string name;
    public Team(string name) { this.name = name; }
    int Check(int answer)
    {
        while (answer < 1 || answer > 5)
        {
            Console.Write("Ваш выбор не корректен. Введите чило от 1 до 5. :");
            answer = Convert.ToInt32(Console.ReadLine());
        }
        return answer;
    }
    int Menu()
    {
        int answer;
        Console.WriteLine("Какую часть дома вы хотите построить?");
        Console.WriteLine("1. Фундамент, 2. Стена, 3. Дверь, 4. Окно, 5. Крыша.");
        Console.Write("Введите правильный выбор - ");
        answer = Convert.ToInt32(Console.ReadLine());
        return Check(answer);
    }
    public void ShowWorker()
    {
        Console.WriteLine($"Строительная бригада - {name} \n");
    }
    public void Build(List<IPart> obj)
    {
        bool check = false;
        int qtyWall = 1, qtyWindow = 1;
        Console.WriteLine();
        switch (Menu())
        {
            case 1:
                foreach (var part in obj)
                {
                    if (part is Basement)
                    {
                        if (part.Status == false)
                        {
                            part.Status = true;
                            Console.WriteLine($"\nКоманда построила {part.ShowPart()}.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"\n{part.ShowPart()} дома уже построено(а).");
                            Console.WriteLine("Проверьте отчет бригадира.");
                            break;
                        }
                    }
                }
                break;
            case 2:
                int wallsBuilt = 0;
                foreach (var part in obj)
                {
                    if (part is Basement && part.Status == true)
                        check = true;
                    else if (part is Basement && part.Status == false)
                    {
                        check = false;
                        break;
                    }
                    if (part is Wall && part.Status == true)
                        wallsBuilt++;
                }
                if (check && wallsBuilt < 4)
                {
                    foreach (var part in obj)
                    {
                        if (part is Wall)
                        {
                            if (part.Status == false)
                            {
                                part.Status = true;
                                Console.WriteLine($"\nКоманда построила {part.ShowPart()} {qtyWall}.");
                                break;
                            }
                            qtyWall++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nСтена дома не может быть построена");
                    Console.WriteLine("Потому что все стены уже построены или отсутствует фундамент.");
                    Console.WriteLine("Смотри отчет бригадира.");
                    break;
                }
                break;
            case 3:
                qtyWall = 0;
                foreach (var part in obj)
                {
                    if (part is Wall && part.Status == true)
                        qtyWall++;
                    if (part is Door && part.Status == false)
                        check = true;
                }
                if (check && qtyWall == 4)
                    foreach (var part in obj)
                    {
                        if (part is Door)
                        {
                            if (part.Status == false)
                            {
                                part.Status = true;
                                Console.WriteLine($"\nКоманда построила {part.ShowPart()}.");
                                break;
                            }
                        }
                    }
                else
                {
                    Console.WriteLine("\nДверь дома не может быть построена.");
                    Console.WriteLine("Потому что она уже построена или не все стены построены.");
                    Console.WriteLine("Смотри отчет бригадира.");

                    break;
                }
                break;
            case 4:
                int windowsBuilt = 0;
                foreach (var part in obj)
                {
                    if (part is Door && part.Status == true)
                        check = true;
                    else if (part is Door && part.Status == false)
                    {
                        check = false;
                        break;
                    }
                    if (part is Window && part.Status == true)
                        windowsBuilt++;
                }
                if (check && windowsBuilt < 4)
                {
                    foreach (var part in obj)
                    {
                        if (part is Window)
                        {
                            if (part.Status == false)
                            {
                                part.Status = true;
                                Console.WriteLine($"\nКоманда построила {part.ShowPart()} {qtyWindow}.");
                                break;
                            }
                            qtyWindow++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nОкно дома не может быть построено.");
                    Console.WriteLine("Потому что они все уже построены или дверь не установлена.");
                    Console.WriteLine("Смотри отчет бригадира.");
                    break;
                }
                break;
            case 5:
                qtyWindow = 0;
                foreach (var part in obj)
                {
                    if (part is Window && part.Status == true)
                        qtyWindow++;
                    if (part is Roof && part.Status == false)
                        check = true;
                }
                if (check && qtyWindow == 4)
                    foreach (var part in obj)
                    {
                        if (part is Roof)
                        {
                            if (part.Status == false)
                            {
                                part.Status = true;
                                Console.WriteLine($"\nКоманда построила {part.ShowPart()}.");
                                Console.WriteLine("Дом построен!!! Смотри отчет бригадира.");
                                break;
                            }
                        }
                    }
                else
                {
                    Console.WriteLine("\nКрыша дома не может быть построена.");
                    Console.WriteLine("Потому что она уже построена или дверь не построена.");
                    Console.WriteLine("Смотри отчет бригадира.");
                    break;
                }
                break;
        }
        Console.WriteLine();
    }
}

class Worker : IWorker
{
    public void ShowWorker() { }
}


class TeamLeader : IWorker
{
    string name;
    public TeamLeader(string name) { this.name = name; }
    string checkStatus(bool status)
    {
        return status == true ? "закончено" : "не построено";
    }
    public void ShowWorker()
    {
        Console.WriteLine($"Бригадир - {name} реализует отчет о строительстве.");
    }
    public void BuildShow(List<IPart> list)
    {
        bool check = false;
        int wall = 1, window = 1;
        Console.WriteLine();
        foreach (var obj in list)
        {
            if (obj is Basement)
                Console.WriteLine($"{obj.ShowPart()} - {checkStatus(obj.Status)}.");
            if (obj is Wall)
                Console.WriteLine($"{obj.ShowPart()}{wall++} - {checkStatus(obj.Status)}.");
            if (obj is Door)
                Console.WriteLine($"{obj.ShowPart()} - {checkStatus(obj.Status)}.");
            if (obj is Window)
                Console.WriteLine($"{obj.ShowPart()}{window++} - {checkStatus(obj.Status)}.");
            if (obj is Roof)
                Console.WriteLine($"{obj.ShowPart()} - {checkStatus(obj.Status)}.");
            if (obj is Roof && obj.Status == true)
                check = true;
        }
        Console.WriteLine();

        if (check)
        {
            Console.WriteLine("\nДом построен!");
            Console.WriteLine("\nРисунок дома:\n");
            Console.WriteLine("  ");
            Console.WriteLine("   __T_______________");
            Console.WriteLine("  /                  \\ ");
            Console.WriteLine(" /____________________\\ ");
            Console.WriteLine(" | __     __     __   |  ");
            Console.WriteLine(" ||__|   |__|   |__|  |  ");
            Console.WriteLine(" | __         __      |  ");
            Console.WriteLine(" ||__|       |  |     |  ");
            Console.WriteLine(" |___________|__|_____|  ");
            Console.WriteLine(" |____________________|  ");
            Console.WriteLine();
        }

    }
}
