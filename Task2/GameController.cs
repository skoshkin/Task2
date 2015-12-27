using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Game;

namespace Task2
{
    public class GameController
    {
        /// <summary>
        /// Участник
        /// </summary>
        public static List<Bug> Bugs { get; set; }

        /// <summary>
        /// Игрок
        /// </summary>
        public static List<Gambler> Gamblers { get; set; }

        /// <summary>
        /// Ставки
        /// </summary>
        public static List<Bet> Bets { get; set; }

        public static List<string> ResultGames { get; set; }

        /// <summary>
        /// Инициализация всех коллекций
        /// </summary>
        public static void InitializeStaticCollection()
        {
            Bets = new List<Bet>();
            ResultGames = new List<string>();

            Bugs = new List<Bug>();//Выделяем память для коллекции участников
            AddBug(new Bug("Участник 1", "T1.png", 1));
            AddBug(new Bug("Участник 2", "T2.png", 2));
            AddBug(new Bug("Участник 3", "T3.png", 3));
            AddBug(new Bug("Участник 4", "T4.png", 4));

            Gamblers = new List<Gambler>();//Выделяем память для коллекции игроков
            AddGambler(new Gambler("Игрок 1", 1));
            AddGambler(new Gambler("Игрок 2", 2));
            AddGambler(new Gambler("Игрок 3", 3));
        }

        /// <summary>
        /// Добавляет участника
        /// </summary>
        /// <param name="_bug"></param>
        /// <returns></returns>
        public static bool AddBug(Bug _bug)
        {
            try
            {
                Bugs.Add(_bug);//добавляем участника в коллекцию
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Добавляет игрока
        /// </summary>
        /// <param name="_gambler"></param>
        /// <returns></returns>
        public static bool AddGambler(Gambler _gambler)
        {
            try
            {
                Gamblers.Add(_gambler);//добавляем игрока в коллекцию
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Метод устанваливает ставки
        /// </summary>
        /// <param name="col"></param>
        public static void SetBets(IEnumerable<Setter> col)
        {
            Bets = new List<Bet>();
            foreach (var item in col)
            {
                var g = Gamblers.Find(s => s.Number == item.Gambler);//Выбираем игрока
                var b = Bugs.Find(s => s.Number == item.Bug);//ВЫбираем участника

                var row = new Bet(b, g, item.Bet);//создаем ставку
                Bets.Add(row);//добавляем ставку
            }
        }

        /// <summary>
        /// Расчитываем выйгрышь
        /// </summary>
        public static void CalcWinning()
        {
            var bugs1 = Bugs.Where(s => s.Position == 1).ToList();//первое место участники
            var summ = Bets.Sum(s => s.Amount);//сумма всех ставок
            //Победившие участники
            var res = "";
            if (bugs1.Count() > 1)
            {
                res = bugs1.Aggregate("В забеге победили участники с номерами ", (current, item) => current + " " + item.Number);
            }
            else
            {
                res = "В забеге победил участник с номером " + bugs1.First().Number;
            }
            ResultGames.Add(res + "\n");//добавили в историю
            
            //-------------- выйгрышь первого игрока -----------------
            try
            {
                var bets1 = Bets.Where(s => bugs1.Any(q => q.Name == s.Bug.Name)).ToList();
                var summBet = (summ / 2) / bets1.Count();
                float totalFirst = (summ / 2);
                summ = summ - totalFirst;//сумма всех ставок
                switch (bets1.Count())
                {
                    case 0:
                        res = "Игроков, которые поставили на 1-е место нет.";
                        break;
                    case 1:
                        res = "Для игрока " + bets1.First().Gambler.Name + " выйгрыш составил " + summBet.ToString("F2") + ".";
                        break;
                    default:
                        var strBest = bets1.Aggregate("", (current, item) => current + " " + item.Gambler.Name);
                        res = "Для игроков: " + strBest + " выйгрыш составил " + summBet.ToString("F2") + " для каждого.";
                        break;
                }
                ResultGames.Add(res + "\n");//добавили в историю
            }
            catch (Exception ex)
            {
                
            }

            ////------------- второе место -----------------
            try
            {
                var bugs2 = Bugs.Where(s => s.Position == 2).ToList();//первое место участники
                var bets2 = Bets.Where(s => bugs2.Any(q => q.Name == s.Bug.Name)).ToList();
                var summBet = (summ * 0.75) / bets2.Count();
                float totalFirst = summ * (float)0.75;
                summ = summ - totalFirst;//сумма всех ставок
                switch (bets2.Count())
                {
                    case 0:
                        res = "Игроков, которые поставили на 2-е место нет.";
                        break;
                    case 1:
                        res = "Для игрока " + bets2.First().Gambler.Name + " выйгрыш составил " + summBet.ToString("F2")+ ".";
                        break;
                    default:
                        var strBest = bets2.Aggregate("", (current, item) => current + " " + item.Gambler.Name);
                        res = "Для игроков: " + strBest + " выйгрыш составил " + summBet.ToString("F2") + " для каждого.";
                        break;
                }
                ResultGames.Add(res + "\n");//добавили в историю
            }
            catch (Exception ex)
            {

            }
            
            ////-----------------третье место--------------
            try
            {
                var bugs3 = Bugs.Where(s => s.Position == 3).ToList();//первое место участники
                var bets3 = Bets.Where(s => bugs3.Any(q => q.Name == s.Bug.Name)).ToList();
                var summBet = (summ) / bets3.Count();
                switch (bets3.Count())
                {
                    case 0:
                        res = "Игроков, которые поставили на 3-е место нет.";
                        break;
                    case 1:
                        res = "Для игрока " + bets3.First().Gambler.Name + " выйгрыш составил " + summBet.ToString("F2") + ".";
                        break;
                    default:
                        var strBest = bets3.Aggregate("", (current, item) => current + " " + item.Gambler.Name);
                        res = "Для игроков: " + strBest + " выйгрыш составил " + summBet.ToString("F2") + " для каждого.";
                        break;
                }
                ResultGames.Add(res + "\n");//добавили в историю
            }
            catch (Exception ex)
            {

            }

            ////-----------------четвертое место--------------
            try
            {
                var bugs4 = Bugs.Where(s => s.Position == 4).ToList(); //первое место участники
                var bets4 = Bets.Where(s => bugs4.Any(q => q.Name == s.Bug.Name)).ToList();
                if (bets4.Any())
                {
                    var strBest = bets4.Aggregate("", (current, item) => current + " " + item.Gambler.Name);
                    if (bets4.Count>1)
                    {
                        res = "Игроки: " + strBest + " проиграли.";
                    }
                    else
                    {
                        res = "Игрок: " + strBest + " проиграл.";
                    }
                    ResultGames.Add(res + "\n"); //добавили в историю
                }
            }
            catch (Exception ex)
            {

            }
        }

    }

    /// <summary>
    /// Класс который связывает Игрока Участинка и ставку (промежуточный)
    /// </summary>
    public class Setter
    {
        public int Gambler { get; set; }

        public int Bug { get; set; }

        public float Bet { get; set; }
    }
}
