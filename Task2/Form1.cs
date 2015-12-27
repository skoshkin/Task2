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

        private int LeftFinish { get; set; }

        private int Delta { get; set; }

        public Form1()
        {
            Setters = new List<Setter>();
            LeftFinish = 700;
            Delta = 10;
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
            GameController.SetBets(Setters);//расчитываем все значения
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

                var imageWidth = 143;

                //анимация движения картинок
                while (true)
                {
                    var nextPosition = GameController.Bugs.Max(s => s.Position) + 1;
                    if (GameController.Bugs.Count(s => s.IsFinish) == 4)
                    {
                        break;
                    }
                    var left1 = bug1.Move(Delta);
                    if (!bug1.IsFinish)
                    {
                        pictureBox_gambler_1.Left = left1;
                        if (left1 + imageWidth >= LeftFinish)
                        {
                            bug1.IsFinish = true;
                            bug1.Position = nextPosition;
                        }
                    }
                    var left2 = bug2.Move(Delta);
                    if (!bug2.IsFinish)
                    {
                        pictureBox_gambler_2.Left = left2;
                        if (left2 + imageWidth >= LeftFinish)
                        {
                            bug2.IsFinish = true;
                            bug2.Position = nextPosition;
                        }
                    }
                    var left3 = bug3.Move(Delta);
                    if (!bug3.IsFinish)
                    {
                        pictureBox_gambler_3.Left = left3;
                        if (left3 + imageWidth >= LeftFinish)
                        {
                            bug3.IsFinish = true;
                            bug3.Position = nextPosition;
                        }
                    }
                    var left4 = bug4.Move(Delta);
                    if (!bug4.IsFinish)
                    {
                        pictureBox_gambler_4.Left = left4;
                        if (left4 + imageWidth >= LeftFinish)
                        {
                            bug4.IsFinish = true;
                            bug4.Position = nextPosition;
                        }
                    }
                    Thread.Sleep(100);
                }
                GameController.CalcWinning();//расчитываем все значения
                result_label.Text = result_label.Text + "\n" + GameController.ResultGames.Aggregate("", (current, item) => current + item);
                GameController.InitializeStaticCollection();
            }
        }

        /// <summary>
        /// Инициализируем картинки и выставляем их 70 px от края
        /// </summary>
        void ImageInitialize()
        {
            //Устанавливаем финишь
            pictureBox_Finish.Left = LeftFinish;
            //Инициализируем картинки
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

        /// <summary>
        /// Событие загрузки формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ImageInitialize();
        }
    }
}