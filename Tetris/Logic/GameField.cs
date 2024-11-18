using System;
using System.Drawing;
using Tetris.Shapes;

namespace Tetris.Logic
{
    //класс с итоговой логикой работы игры
    public class GameField
    {
        //событие установки фигуры
        public event Action EventShapeSet;
        //поле текущей фигуры
        private BasicShape currentShape;
        //свойство текущей фигуры
        public BasicShape CurrentShape
        {
            get => currentShape;
            private set
            {
                currentShape = value;
                currentShape.InitialPosition();
            }
        }

        //поле игрового "стакана"
        public TetrisCup TetrisCup { get; }
        //поле окончания игры
        public bool GameOver { get; private set; }
        //поле текущего счета
        public int Score { get; private set; }

        //конструктор
        public GameField()
        {
            TetrisCup = new TetrisCup(22, 10);
            CurrentShape = ShapeGen.Get(new IShape());
        }

        //проверка, сталкиваются ли фигуры
        private bool ShapeCollides()
        {
            foreach (Point item in CurrentShape.BlockPositions())
            {
                if (!TetrisCup.IsEmpty(item.X, item.Y))
                    return true;
            }
            return false;
        }

        //попытка повернуть фигуру
        public void TryRotate()
        {
            CurrentShape.Rotate();

            if (ShapeCollides())
                CurrentShape.UndoRotate();
        }

        //попытка передвинуть фигуру влево
        public void TryMoveLeft()
        {
            CurrentShape.Move(0, -1);

            if (ShapeCollides())
                CurrentShape.Move(0, 1);
        }
        //попытка передвинуть фигуру вправо
        public void TryMoveRight()
        {
            CurrentShape.Move(0, 1);

            if (ShapeCollides())
                CurrentShape.Move(0, -1);
        }

        //метод для проверки, окончена ли игра
        private bool CheckGameOver() => !(TetrisCup.IsRowEmpty(0) && TetrisCup.IsRowEmpty(1));

        //метод для установки фигуры в стакан
        private void SetShape()
        {
            EventShapeSet?.Invoke();

            foreach (Point item in CurrentShape.BlockPositions())
                TetrisCup[item.X, item.Y] = (int)CurrentShape.FigureShape;

            Score += TetrisCup.ClearFullRows();
            if (CheckGameOver())
                GameOver = true;
            else
                CurrentShape = ShapeGen.Get(CurrentShape);
            
            EventShapeSet?.Invoke();
        }

        //попытка передвинуть фигуру вниз
        public void TryMoveDown()
        {
            CurrentShape.Move(1, 0);

            if (ShapeCollides())
            {
                CurrentShape.Move(-1, 0);
                SetShape();
            }
        }

        //метод для расчета количества ячеек до первой нижней ячейки, заполненной фигурой
        private int BlockDropValue(Point point)
        {
            int emptyCells = 0;

            while(TetrisCup.IsEmpty(point.X + emptyCells + 1, point.Y))
                emptyCells++;

            return emptyCells;
        }

        //метод для расчета количества ячеек для текущей фигуры
        private int ShapeDropDistance()
        {
            int dropValue = TetrisCup.Rows;
            foreach (Point item in CurrentShape.BlockPositions())
                dropValue = System.Math.Min(dropValue, BlockDropValue(item));
            return dropValue;
        }
        
        //метод для быстрой установки фигуры
        public void DropShape()
        {
            CurrentShape.Move(ShapeDropDistance(), 0);
            SetShape();
        }
    }
}
