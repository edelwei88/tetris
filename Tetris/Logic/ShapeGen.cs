using System;
using Tetris.Shapes;

namespace Tetris.Logic
{
    public static class ShapeGen
    {
        //объект Random
        private static Random random = new Random(DateTime.Now.Millisecond);

        //массив со всеми возможными фигуры
        private static BasicShape[] shapes = new BasicShape[]
        {
            new IShape(),
            new JShape(),
            new LShape(),
            new OShape(),
            new SShape(),
            new TShape(),
            new ZShape()
        };

        //метод для получения новой случайной фигуры
        public static BasicShape Get(BasicShape currentShape)
        {
            BasicShape newShape;
            do
            {
                newShape = shapes[random.Next(shapes.Length)];
            }
            while (currentShape.FigureShape == newShape.FigureShape);

            return newShape;
        }
    }
}
