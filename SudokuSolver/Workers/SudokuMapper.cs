using SudokuSolver.Data;

namespace SudokuSolver.Workers
{
    public class SudokuMapper
    {
        public SudokuMap Find(int row, int col)
        {
            SudokuMap sudokuMap = new SudokuMap();

            if(row >= 0 && row < 9 && col >= 0 && col < 9)
            {
                sudokuMap.StartRow = (row / 3) * 3;
                sudokuMap.StartCol = (col / 3) * 3;
            }

            return sudokuMap;
        }
    }
}