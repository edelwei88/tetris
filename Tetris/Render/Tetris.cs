using System.Windows.Forms;
using System.Drawing;
using Tetris.Logic;
using Tetris.Struct;
using System;

namespace Tetris.Render
{
    public class Tetris : Control
    {
        //событие окончание игры
        public event Action EventGameOver;

        //свойство счета
        public int Score
        {
            get
            {
                return gameField.Score;
            }
        }

        //поле для буферизованной графики
        BufferedGraphics bg;
        Graphics g;
        //поле игрового "стакана"
        public GameField gameField;
        //размер ячейки при отрисовке
        int cellSize = 30;
        //текущая сложность
        Difficulties diff = Difficulties.Easy;
        //таймер игры
        Timer timer;

        //цвета фигур
        readonly Color[] colors;

        //конструктор
        public Tetris(Color[] colors, bool playMusic)
        {
            this.colors = colors;


            gameField = new GameField();
            gameField.EventShapeSet += InvalidateSetShapes;
            gameField.EventShapeSet += CheckGameOver;

            timer = new Timer();
            timer.Interval = (int)diff;
            timer.Tick += Tetris_Tick;
            timer.Start();

        }

        //метод игрового "тика"
        private void Tetris_Tick(object sender, System.EventArgs e)
        {
            InvalidateCurrentShape();

            gameField.TryMoveDown();
            ChangeDifficulty();

            InvalidateCurrentShape();
        }

        //отрисовка игры
        protected override void OnPaint(PaintEventArgs e)
        {
            g.Clear(BackColor);

            //отрисовка текущей фигуры
            foreach (var item in gameField.CurrentShape.BlockPositionsToDraw())
                g.FillRectangle(new SolidBrush(colors[(int)gameField.CurrentShape.FigureShape - 1]), item.Y * cellSize + cellSize, item.X * cellSize - cellSize, cellSize, cellSize);

            //отрисовка установленных фигур
            for (int i = 0; i < gameField.TetrisCup.Rows; i++)
                for (int j = 0; j < gameField.TetrisCup.Cols; j++)
                    if (gameField.TetrisCup[i, j] != 0)
                        g.FillRectangle(new SolidBrush(colors[gameField.TetrisCup[i, j] - 1]), j * cellSize + cellSize, i * cellSize - cellSize, cellSize, cellSize);


            //отрисовка обводки игрового поля
            for (int i = 0; i < gameField.TetrisCup.Rows; i++)
            {
                for (int j = 0; j < gameField.TetrisCup.Cols + 2; j++)
                {
                    if (i == 0 || i == gameField.TetrisCup.Rows - 1)
                    {
                        g.FillRectangle(Brushes.DarkGray, new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));
                        g.FillRectangle(Brushes.Gray, new Rectangle(j * cellSize + 2, i * cellSize + 2, cellSize - 2, cellSize - 2));
                    }

                    if (j == 0 || j == gameField.TetrisCup.Cols + 1)
                    {
                        g.FillRectangle(Brushes.DarkGray, new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));
                        g.FillRectangle(Brushes.Gray, new Rectangle(j * cellSize + 2, i * cellSize + 2, cellSize - 2, cellSize - 2));
                    }
                }
            }

            //отрисовка игрового поля
            for (int i = 0; i < gameField.TetrisCup.Rows; i++)
                for (int j = 0; j < gameField.TetrisCup.Cols + 2; j++)
                    g.DrawRectangle(Pens.SlateGray, new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));

            bg.Render(e.Graphics);
            base.OnPaint(e);
        }

        //кнопки для кнопок
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {

            if (gameField.GameOver)
                return;

            InvalidateCurrentShape();

            switch (e.KeyCode)
            {
                case Keys.Up:
                    gameField.TryRotate();
                    break;
                case Keys.Left:
                    gameField.TryMoveLeft();
                    break;
                case Keys.Right:
                    gameField.TryMoveRight();
                    break;
                case Keys.Down:
                    gameField.TryMoveDown();
                    break;
                case Keys.Space:
                    gameField.DropShape();
                    break;
                default:
                    return;
            }

            InvalidateCurrentShape();
        }

        //метод для отрисовки текущей фигуры
        private void InvalidateCurrentShape()
        {
            foreach (var item in gameField.CurrentShape.BlockPositionsToDraw())
                Invalidate(new Rectangle(item.Y * cellSize + cellSize, item.X * cellSize - cellSize, cellSize, cellSize));
        }

        //метод для отрисовки установленных фигур
        private void InvalidateSetShapes()
        {
            for (int i = 0; i < gameField.TetrisCup.Rows; i++)
                for (int j = 0; j < gameField.TetrisCup.Cols; j++)
                    if (gameField.TetrisCup[i, j] != 0)
                        Invalidate(new Region(new Rectangle(j * cellSize + cellSize, i * cellSize - cellSize, cellSize, cellSize)));
        }

        //метод для изменения сложности
        private void ChangeDifficulty()
        {
            if (Score < 20 && Score >= 10)
                timer.Interval = (int)Difficulties.Normal;
            else if (Score < 30 && Score >= 20)
                timer.Interval = (int)Difficulties.Hard;
            else if (Score >= 30)
                timer.Interval = (int)Difficulties.VeryHard;
        }

        //метод проверки окончания игры
        private void CheckGameOver()
        {
            if (gameField.GameOver)
            {
                timer.Stop();
                timer.Tick -= Tetris_Tick;
                timer.Dispose();
                EventGameOver?.Invoke();
            }
        }

        public void TimerUnsub()
        {
            if (gameField.GameOver == false)
            {
                timer.Stop();
                timer.Tick -= Tetris_Tick;
                timer.Dispose();
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            bg = BufferedGraphicsManager.Current.Allocate(Graphics.FromImage(new Bitmap(Width, Height)), new Rectangle(0, 0, Width, Height));
            g = bg.Graphics;
        }
    }
}
