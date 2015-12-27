using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2.Game
{
    /// <summary>
    /// Участник
    /// </summary>
    public class Bug
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        /// <summary>
        /// Общее смещение
        /// </summary>
        private int LeftTotal { get; set; }
        /// <summary>
        /// Финишировал ли участник
        /// </summary>
        public bool IsFinish { get; set; }
        /// <summary>
        /// Номер под которым финишировал
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Конструктор класса (создаем участника)
        /// </summary>
        /// <param name="_name">Имя</param>
        /// <param name="_image">Картинка</param>
        /// <param name="_number">Номер</param>
        public Bug(string _name, string _image, int _number)
        {
            LeftTotal = 0;
            Image = _image;
            Name = _name;
            Number = _number;
        }

        /// <summary>
        /// Считает смещение
        /// </summary>
        /// <param name="delta">Дельта(длинна шага)</param>
        /// <returns></returns>
        public int Move(int delta)
        {
            var leftStep = delta * RandomPosition();
            LeftTotal = LeftTotal + leftStep;
            return LeftTotal;
        }

        /// <summary>
        /// Генерирует рэндомное значение
        /// </summary>
        private int RandomPosition()
        {
            Thread.Sleep(20);
            var rnd = new Random();
            return rnd.Next(0, 5);
        }
    }
}