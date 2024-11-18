using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;

namespace TetrisGame
{
    public partial class Game : Form
    {
        //поле игры
        Tetris.Render.Tetris tetris;
        //объект музыкального плеера
        WindowsMediaPlayer player = new WindowsMediaPlayer();

        //конструктор
        public Game(Color[] colors, bool playMusic)
        {
            InitializeComponent();

            tetris = new Tetris.Render.Tetris(colors, playMusic);
            tetris.Size = new Size(500,700);
            Controls.Add(tetris);
            if (playMusic)
            {
                player.URL = "tetris.mp3";
                player.settings.setMode("loop", true);
                player.controls.play();
            } 

            tetris.gameField.EventShapeSet += UpdateScore;
            tetris.EventGameOver += GameOver;

            tetris.Focus();
        }

        //метод для обновления счета
        private void UpdateScore()
        {
            label1.Text = $"Очки: {tetris.Score}";
        }

        //метод окончания игры
        private void GameOver()
        {
            player.controls.stop();
            using (var dialog = new Result(tetris.gameField.Score))
            {
                dialog.ShowDialog();
                Close();
            }
        }

        //метод для закрытия окна
        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.controls.stop();
            tetris.TimerUnsub();
        }
    }
}
