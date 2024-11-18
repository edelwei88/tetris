namespace Tetris
{
    //класс игрового "стакана"
    public class TetrisCup
    {
        //поле для игрового "стакана"
        private readonly int[,] cup;
        //количество строк в поле
        public int Rows { get; }
        //количество колонок в поле
        public int Cols { get; }

        //итератор по полю "стакана"
        public int this[int i, int j]
        {
            get => cup[i, j];
            set => cup[i, j] = value;
        }

        //конструктор
        public TetrisCup(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            cup = new int[rows, cols];
        }

        //проверка, находится ли ячейка в "стакане"
        public bool IsInCup(int row, int col) => row >= 0 && row < Rows && col >= 0 && col < Cols;
        //проверка, является ли ячейка "стакана" пустой
        public bool IsEmpty(int row, int col) => IsInCup(row, col) && cup[row, col] == 0;
        //проверка, является ли строка "стакана" полной
        public bool IsRowFull(int row)
        {
            for (int i = 0; i < Cols; i++)
                if (cup[row, i] == 0)
                    return false;
            return true;
        }
        //проверка, является ли строка "стакана" пустой
        public bool IsRowEmpty(int row)
        {
            for (int i = 0; i < Cols; i++)
                if (cup[row, i] != 0)
                    return false;
            return true;
        }

        //метод для очистки строки "стакана"
        private void ClearRow(int row)
        {
            for (int i = 0; i < Cols; i++)
                cup[row, i] = 0;
        }

        //метод для передвижения строки "стакана" вниз
        private void MoveRowDown(int row, int delta)
        {
            for (int i = 0; i < Cols; i++)
            {
                cup[row + delta, i] = cup[row, i];
                cup[row, i] = 0;
            }
        }

        //метод для очистки "стакана" от полных ячееи и передвижения остальных ячеек вниз
        public int ClearFullRows()
        {
            int cleared = 0;

            for (int i = Rows - 1; i >= 0; i--)
                if (IsRowFull(i))
                {
                    ClearRow(i);
                    cleared++;
                }
                else if (cleared > 0)
                    MoveRowDown(i, cleared);

            return cleared;
        }

    }
}
