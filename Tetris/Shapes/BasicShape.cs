using System.Drawing;
using Tetris.Struct;
using System.Collections.Generic;

namespace Tetris.Shapes
{
    //абстрактный класс, от которого наследуются все другие фигуры
    public abstract class BasicShape
    {
        //все варианты разворота фигуры
        protected abstract Point[][] Blocks { get; }
        //начальная позиция фигуры на поле
        protected abstract Point StartPosition { get; }
        //поле вида фигуры
        public abstract Shape FigureShape { get; }

        //текущий поворот фигуры
        private int rotation;
        //текущая позиция фигуры
        private Point position;

        //конструктор
        public BasicShape()
        {
            position = new Point(StartPosition.X, StartPosition.Y);
        }

        //получение позиций точек фигуры
        public IEnumerable<Point> BlockPositions()
        {
            foreach (Point item in Blocks[rotation])
                yield return new Point(item.X + position.X, item.Y + position.Y);
        }
        public IEnumerable<Point> BlockPositionsToDraw()
        {
            foreach (Point item in Blocks[rotation])
                if (item.X + position.X > 1)
                    yield return new Point(item.X + position.X, item.Y + position.Y);
        }

        //поворот фигуры
        public void Rotate()
        {
            rotation = (rotation + 1) % Blocks.Length;
        }

        //поворот фигуры в обратную сторону
        public void UndoRotate()
        {
            if (rotation == 0)
                rotation = Blocks.Length - 1;
            else
                rotation--;
        }

        //передвижение фигуры
        public void Move(int r, int c)
        {
            position.X += r;
            position.Y += c;
        }

        //установка начальной позиции для фигуры
        public void InitialPosition()
        {
            rotation = 0;
            position.X = StartPosition.X;
            position.Y = StartPosition.Y;
        }

    }
}
