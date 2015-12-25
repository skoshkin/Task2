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
        public int Position { get; set; }

        /// <summary>
        /// Конструктор класса (создаем участника)
        /// </summary>
        /// <param name="_name">Имя</param>
        /// <param name="_image">Картинка</param>
        /// <param name="_number">Номер</param>
        public Bug(string _name, string _image, int _number)
        {
            Position = 0;
            Image = _image;
            Name = _name;
            Number = _number;
        }

        public void Move()
        {
            
        }

        /// <summary>
        /// Генерирует рэндомное значение
        /// </summary>
        public void RandomPosition()
        {
            Thread.Sleep(20);
            var rnd = new Random();
            Position = rnd.Next(0,10);
        }
    }
}