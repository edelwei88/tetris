using System.Drawing;
using Tetris.Struct;

namespace Tetris.Shapes
{
    public class OShape : BasicShape
    {
        private readonly Point[][] blocks = new Point[][]
        {
            new Point[] { new Point(0, 0), new Point(0, 1), new Point (1, 0), new Point(1, 1) }
        };
        protected override Point[][] Blocks => blocks;
        protected override Point StartPosition => new Point(0, 4);
        public override Shape FigureShape => Shape.O;
    }
}
