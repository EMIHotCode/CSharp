using System;
using System.Collections.Generic;

namespace HomeWork9
{

    internal class Program
    {
        static int Check(int answer)
        {
            while (answer < 1 || answer > 3)
            {
                Console.WriteLine("Вы ввели неправильный выбор. Введите число от 1 до 3");
                answer = Convert.ToInt32(Console.ReadLine());
            }
            return answer;
        }
        static int Menu()
        {
            int answer;
            Console.WriteLine("Какое действие вы хотите совершить?");
            Console.WriteLine("1. Посмотреть отчет о строительстве дома.");
            Console.WriteLine("2. Построить какую нибудь часть дома.");
            Console.WriteLine("3. Выход.");
            Console.Write("Ваш выбор - ");
            answer = Convert.ToInt32(Console.ReadLine());
            return Check(answer);
        }
        static void Main(string[] args)
        {
            House house = new House();
            TeamLeader teamLeader = new TeamLeader("Иванов И.И.");
            Team team = new Team("SUPER Comanda Workers");
            Worker worker = new Worker(); 
            Console.WriteLine("Команда строителей:");
            teamLeader.ShowWorker();
            team.ShowWorker();
            do
            {
                switch (Menu())
                {
                    case 1:
                        teamLeader.BuildShow(house.GetList());
                        break;
                    case 2:
                        team.Build(house.GetList());
                        break;
                    case 3:
                        return;
                }
            } while (true);
        }
    }
}
