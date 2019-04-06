using System;
using System.IO;
using System.Linq;

namespace SudokuSolver.Workers
{
    public class SudokuFileReader
    {
        public int[,] ReadFile(string filename)
        {
            int[,] sudokuBoard = new int[9, 9];

            try
            {
                string[] sudokuBoardLines = File.ReadAllLines(filename);

                for (int row = 0; row < 9; row++)
                {
                    string[] cells = sudokuBoardLines[row].Split("|").Skip(1).Take(9).ToArray();
                    if(cells.Length != 9) throw new Exception("The number of elements in row " + row + "does not equal to 9.");

                    for (int col = 0; col < 9; col++)
                    {
                        sudokuBoard[row, col] = cells[col].Trim() == "" ? 0 : Convert.ToInt16(cells[col]);
                    }
                    
                }
                
            }
            catch (Exception exception)
            {
                throw new Exception("Something went wrong while parsing the file: " + exception.Message);
            }

            return sudokuBoard;
        }
    }
}