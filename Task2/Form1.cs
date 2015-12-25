using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task2.Game;

namespace Task2
{
    public partial class Form1 : Form
    {
        private List<Setter> Setters { get; set; }
        public Form1()
        {
            Setters = new List<Setter>();
            InitializeComponent();
        }

        /// <summary>
        /// Устанавливает ставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void set_bet_Click(object sender, EventArgs e)
        {
            var bugChecked = groupBox_bug.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var gamblerChecked = groupBox_gambler.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var betStr = textbox_bet.Text;
            int bugCheckedId = -1, gamblerCheckedId = -1;
            float betFloat = -1;

            try
            {
                betFloat = float.Parse(betStr);
                bugCheckedId = int.Parse(bugChecked.Name.Replace("radioButton_bug_", ""));
                gamblerCheckedId = int.Parse(gamblerChecked.Name.Replace("radioButton_gambler_", ""));
            }
            catch (Exception)
            {
                MessageBox.Show("Не верный формат ставки, введите корректное число.", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            if (Setters.Any(s => s.Gambler == gamblerCheckedId))//Проверяем делал ли игрок уже ставку, если да удаляем её
            {
                Setters.Remove(Setters.First(s => s.Gambler == gamblerCheckedId));
            }

            Setters.Add(new Setter()
            {
                Bet = betFloat,
                Gambler = gamblerCheckedId,
                Bug = bugCheckedId
            });

            switch (gamblerCheckedId)
            {
                case 1:
                    label_gambler_1.Text = "Игрок 1 сделал ставку в размере " + betFloat + " р. на участника " + bugCheckedId;
                    break;
                case 2:
                    label_gambler_2.Text = "Игрок 2 сделал ставку в размере " + betFloat + " р. на участника " + bugCheckedId;
                    break;
                case 3:
                    label_gambler_3.Text = "Игрок 3 сделал ставку в размере " + betFloat + " р. на участника " + bugCheckedId;
                    break;
            }
        }

        /// <summary>
        /// Начинает забег тараканов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, EventArgs e)
        {
            GameController.CalculateGame(Setters);//расчитываем все значения
            var Bets = GameController.Bets;
            if (Bets.Count != 3)
            {
                var text = "";
                if (Bets.All(s => s.Gambler.Number != 1))
                {
                    text = "Игрок 1 не сделал ставку.\n";
                }

                if (Bets.All(s => s.Gambler.Number != 2))
                {
                    text = text + "Игрок 2 не сделал ставку.\n";
                }

                if (Bets.All(s => s.Gambler.Number != 3))
                {
                    text = text + "Игрок 3 не сделал ставку.\n";
                }

                MessageBox.Show(text, "Соревнования не состоялось", MessageBoxButtons.OK);
            }
            else
            {
                var bug1 = GameController.Bugs.First(s => s.Number == 1);
                var bug2 = GameController.Bugs.First(s => s.Number == 2);
                var bug3 = GameController.Bugs.First(s => s.Number == 3);
                var bug4 = GameController.Bugs.First(s => s.Number == 4);
                //анимация движения картинок
                for (int i =0; i < 10; i++)
                {
                    if (bug1.Number == 1 && bug1.Position >= i)
                    {
                        pictureBox_gambler_1.Left = i*70;
                    }
                    if (bug2.Number == 2 && bug2.Position >= i)
                    {
                        pictureBox_gambler_2.Left = i*70;
                    }
                    if (bug3.Number == 3 && bug3.Position >= i)
                    {
                        pictureBox_gambler_3.Left = i*70;
                    }
                    if (bug4.Number == 4 && bug4.Position >= i)
                    {
                        pictureBox_gambler_4.Left = i*70;
                    }
                    Thread.Sleep(300);
                }
                
                result_label.Text = GameController.ResultGames.Aggregate("", (current, item) => current + item);
            }

        }

        /// <summary>
        /// Событие загрузки формы, инициализируем картинки и выставляем их 70 px от края
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in GameController.Bugs)
            {
                var image = Image.FromFile(item.Image);
                switch (item.Number)
                {
                    case 1:
                        pictureBox_gambler_1.Image = image;
                        pictureBox_gambler_1.Height = image.Height;
                        pictureBox_gambler_1.Width = image.Width;
                        pictureBox_gambler_1.Left = 0;
                        break;
                    case 2:
                        pictureBox_gambler_2.Image = image;
                        pictureBox_gambler_2.Height = image.Height;
                        pictureBox_gambler_2.Width = image.Width;
                        pictureBox_gambler_2.Left = 0;
                        break;
                    case 3:
                        pictureBox_gambler_3.Image = image;
                        pictureBox_gambler_3.Height = image.Height;
                        pictureBox_gambler_3.Width = image.Width;
                        pictureBox_gambler_3.Left = 0;
                        break;
                    case 4:
                        pictureBox_gambler_4.Image = image;
                        pictureBox_gambler_4.Height = image.Height;
                        pictureBox_gambler_4.Width = image.Width;
                        pictureBox_gambler_4.Left = 0;
                        break;
                }

            }
        }
    }
}