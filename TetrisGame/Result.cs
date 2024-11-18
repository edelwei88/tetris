using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Tetris.Score;
using Newtonsoft.Json;

namespace TetrisGame
{
    public partial class Result : Form
    {
        //счет
        int _score;
        //путь до файла
        string _path = @".\scores.json";
        public Result(int score)
        {
            InitializeComponent();
            _score = score;
            label2.Text = $"Счет: {score}";
        }

        //добавление результатов в файл
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length < 4)
            {
                MessageBox.Show("Слишком короткое имя");
                return;
            }

            List<ScoreEntry> scores;

            if (!File.Exists(_path))
            {
                File.Create(_path).Close();
                scores = new List<ScoreEntry>
                {
                    new ScoreEntry(textBox1.Text, _score)
                };
            }
            else
            {
                scores = JsonConvert.DeserializeObject<List<ScoreEntry>>(File.ReadAllText(_path));
                scores.Add(new ScoreEntry(textBox1.Text, _score));
            }

            File.WriteAllText(_path, JsonConvert.SerializeObject(scores, Formatting.Indented));
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
