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
        public static void CalculateGame(IEnumerable<Setter> col)
        {
            Bets = new List<Bet>();
            foreach (var item in col)
            {
                var g = Gamblers.Find(s => s.Number == item.Gambler);//Выбираем игрока
                var b = Bugs.Find(s => s.Number == item.Bug);//ВЫбираем участника

                var row = new Bet(b, g, item.Bet);//создаем ставку
                Bets.Add(row);//добавляем ставку
            }

            //Генерируем случайные числа для каждого участника
            foreach (var item in Bugs)
            {
                item.RandomPosition();
            }

            //Определяем побудителей 
            //-------------- первое место -----------------
            var Positions = Bugs.OrderByDescending(s=>s.Position).Select(s=>s.Position).Distinct().ToList();//Отсортировали по убыванию взяли тока уникальные значения
            var bugs = Bugs.Where(s => s.Position == Positions[0]);

            var res = "";
            if (bugs.Count() > 1)
            {
                res = bugs.Aggregate("В забеге победили участники с номерами ", (current, item) => current + " " + item.Number);
            }
            else
            {
                res = "В забеге победил участник с номером " + bugs.First().Number;
            }
            ResultGames.Add(res + "\n");//добавили в историю
            var summ = Bets.Sum(s => s.Amount);//сумма всех ставок
            var first = (summ / 2) / bugs.Count();
            var totalFirst = (summ/2);
            if (bugs.Count() > 1)
            {
                res = "1-е место. Выйгрышь составил " + first.ToString("F2") + ". Количество победителей: " + bugs.Count();
            }
            else
            {
                res = "1-е место. Выйгрышь составил " + first.ToString("F2") + ". Количество победителей: 1";
            }
            ResultGames.Add(res + "\n");//добавили в историю

            //------------- второе место -----------------

            bugs = Bugs.Where(s => s.Position == Positions[1]);
            summ = summ - totalFirst;//сумма всех ставок
            var two = (summ*0.75) / bugs.Count();
            var totalTwo = (summ * 0.75);
            if (bugs.Count() > 1)
            {
                res = "2-е место. Выйгрышь составил " + two.ToString("F2") + ". Количество победителей: " + bugs.Count();
            }
            else
            {
                res = "2-е место. Выйгрышь составил " + two.ToString("F2") + ". Количество победителей: 1";
            }
            ResultGames.Add(res + "\n");//добавили в историю


            //-----------------третье место--------------
            bugs = Bugs.Where(s => s.Position == Positions[1]);
            summ = summ - (float)totalTwo;//сумма всех ставок
            var free = (summ) / bugs.Count();
            if (bugs.Count() > 1)
            {
                res = "3-е место. Выйгрышь составил " + free.ToString("F2") + ". Количество победителей: " + bugs.Count();
            }
            else
            {
                res = "3-е место. Выйгрышь составил " + free.ToString("F2") + ". Количество победителей: 1";
            }
            ResultGames.Add(res + "\n");//добавили в историю

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
