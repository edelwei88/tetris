using System;

namespace Tetris.Score
{
    //класс для счета
    [Serializable]
    public class ScoreEntry
    {
        //поле имени игрока
        public string Name { get; set; }
        //поле счета игрока
        public int Score { get; set; }

        //конструктор
        public ScoreEntry(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}
