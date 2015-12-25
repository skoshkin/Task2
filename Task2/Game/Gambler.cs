using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Game
{
    /// <summary>
    /// Игрок
    /// </summary>
    public class Gambler
    {
        public string Name { get; set; }

        public int Number { get; set; }

        /// <summary>
        /// Конструктор класса (создаем игрока)
        /// </summary>
        /// <param name="_name">Имя</param>
        /// <param name="_number">Номер</param>
        public Gambler(string _name, int _number)
        {
            Name = _name;
            Number = _number;
        }
    }
}
