using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisGame
{

    public partial class MainMenu : Form
    {
        //текущая цветовая схема
        Color[] gameColor = new Color[]
            {
                Color.LightSkyBlue,
                Color.Bisque,
                Color.LightPink,
                Color.LightBlue,
                Color.Orange,
                Color.LightGreen,
                Color.Purple
            };

        //играть ли музыку
        bool music = true;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new Game(gameColor, music))
            {
                Hide();
                dialog.ShowDialog();
                Show();
            }

        }

        //установка новых настроке
        private void button2_Click(object sender, EventArgs e)
        {
            using ( var dialog = new Settings())
            {
                if (dialog.ShowDialog() == DialogResult.Cancel)
                    return;

                gameColor = dialog.SetColor;
                music = dialog.Music;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var dialog = new Leaderboard())
                dialog.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var dialog = new Help())
                dialog.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
