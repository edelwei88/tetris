using System.Drawing;
using Tetris.Struct;

namespace Tetris.Shapes
{
    public class IShape : BasicShape
    {
        private readonly Point[][] blocks = new Point[][]
        {
            new Point[] {new Point(1, 0), new Point(1, 1), new Point (1, 2), new Point(1, 3)},
            new Point[] {new Point(0, 2), new Point(1, 2), new Point (2, 2), new Point(3, 2)},
            new Point[] {new Point(2, 0), new Point(2, 1), new Point (2, 2), new Point(2, 3)},
            new Point[] {new Point(0, 1), new Point(1, 1), new Point (2, 1), new Point(3, 1)},
        };
        protected override Point[][] Blocks => blocks;
        protected override Point StartPosition => new Point(0, 3);
        public override Shape FigureShape => Shape.I;
    }
}
