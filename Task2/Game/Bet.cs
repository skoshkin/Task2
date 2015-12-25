using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Game
{
    /// <summary>
    /// Ставка
    /// </summary>
    public class Bet
    {
        public Gambler Gambler { get; set; }

        public Bug Bug { get; set; }

        public float Amount { get; set; }

        /// <summary>
        /// Конструктор класса (создаем ставку)
        /// </summary>
        /// <param name="_bug">Участник</param>
        /// <param name="_gambler">Игрок</param>
        /// <param name="_amount">Ставка</param>
        public Bet(Bug _bug, Gambler _gambler, float _amount)
        {
            Bug = _bug;
            Gambler = _gambler;
            Amount = _amount;
        }
    }
}
