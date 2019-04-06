using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using SudokuSolver.Workers;

namespace SudokuSolver.Strategies
{
    public class SimpleMarkUpStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _sudokuMapper;

        public SimpleMarkUpStrategy(SudokuMapper sudokuMapper)
        {
            _sudokuMapper = sudokuMapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    if (sudokuBoard[row, col] == 0 || sudokuBoard[row, col].ToString().Length > 1)
                    {
                        var possibilityInRowAndCol = GetPossibilityInRowAndCol(sudokuBoard, row, col);
                        var possibilityInBlock = GetPossibilityInBlock(sudokuBoard, row, col);

                        sudokuBoard[row, col] = GetPossibilityIntersection(possibilityInRowAndCol, possibilityInBlock);
                    }
                }
            }

            return sudokuBoard;
        }

        private string GetPossibilityInRowAndCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            int[] possibilities = {1, 2, 3, 4, 5, 6, 7, 8, 9};

            for (int col = 0; col < 9; col++)
            {
                if (IsValidSingle(sudokuBoard[givenRow, col]))
                {
                    possibilities[sudokuBoard[givenRow, col] - 1] = 0;
                }
            }

            for (int row = 0; row < 9; row++)
            {
                if (IsValidSingle(sudokuBoard[row, givenCol]))
                {
                    possibilities[sudokuBoard[row, givenCol] - 1] = 0;
                }
            }

            return String.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0));
        }

        private string GetPossibilityInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var sudokuMap = _sudokuMapper.Find(givenRow, givenCol);

            for (int row = sudokuMap.StartRow; row < sudokuMap.StartRow + 3; row++)
            {
                for (int col = sudokuMap.StartCol; col < sudokuMap.StartCol + 3; col++)
                {
                    if (IsValidSingle(sudokuBoard[row, col]))
                    {
                        possibilities[sudokuBoard[row, col] - 1] = 0;
                    }
                }
            }

            var result = possibilities.Where(item => item != 0).ToArray();

            return string.Join(string.Empty, result);
        }

        private int GetPossibilityIntersection(string possibilityInRowAndCol, string possibilityInBlock)
        {
            char[] possibilityInRowAndColCharArray = possibilityInRowAndCol.ToCharArray();
            char[] possibilityInBlockCharArray = possibilityInBlock.ToCharArray();

            var possibilitySubset = possibilityInRowAndColCharArray.Intersect(possibilityInBlockCharArray);

            return Convert.ToInt32(string.Join(string.Empty, possibilitySubset));
        }

        private bool IsValidSingle(int cellDigit)
        {
            return cellDigit != 0 && cellDigit.ToString().Length == 1;
        }
    }
}