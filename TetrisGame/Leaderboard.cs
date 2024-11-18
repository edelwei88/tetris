using System.Collections.Generic;
using System.Windows.Forms;
using Tetris.Score;
using System.IO;


namespace TetrisGame
{
    public partial class Leaderboard : Form
    {
        //поле результатов
        List<ScoreEntry> scores;

        public Leaderboard()
        {
            InitializeComponent();

            //если нет результатов, закрыть форму
            try
            {
                scores = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ScoreEntry>>(File.ReadAllText(@".\scores.json"));
            }
            catch
            {
                MessageBox.Show("Сначала сыграйте свою первую игру и загрузите результат");
                scores = new List<ScoreEntry>();

            }

            //сортировка по убыванию
            scores.Sort(new DescScoreComparer());

            //количество результатов
            int length = scores.Count < 10 ? scores.Count : 10;

            //вывод результатов
            for (int i = 0; i < length; i++)
            {
                dataGridView1.Rows.Add(scores[i].Name, scores[i].Score);
            }

        }
    }

    class DescScoreComparer : IComparer<ScoreEntry>
    {
        public int Compare (ScoreEntry x, ScoreEntry y)
        {
            return y.Score - x.Score;
        }

    }

}
