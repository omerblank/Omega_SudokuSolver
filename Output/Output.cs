using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Output
{
    private Board sudokuBoard;

    /// <summary>
    /// this function creates a new Output object
    /// </summary>
    /// <param name="input"></param>
    public Output(string input)
    {
        sudokuBoard = new Board(input);
    }

    /// <summary>
    /// this function prints the board
    /// </summary>
    public void PrintBoard()
    {
        for (int i = 0; i < Constants.SIDE; i++)
        {
            for (int j = 0; j < Constants.SIDE; j++)
            {
                if (j < Constants.SIDE - 1)
                    Console.Write(sudokuBoard.Cells[i, j] + " | ");
                else
                    Console.Write(sudokuBoard.Cells[i, j]);
            }
            Console.WriteLine();
            for (int k = 0; i < Constants.SIDE - 1 && k < Constants.SIDE * (Math.Sqrt(Constants.SIDE) + 1) - Math.Sqrt(Constants.SIDE); k++)
            {
                if ((k - Math.Sqrt(Constants.SIDE) + 1) % ((int)Math.Sqrt(Constants.SIDE) + 1) == 0 || (k + 1) == Math.Sqrt(Constants.SIDE) && k < Constants.SIDE * Math.Sqrt(Constants.SIDE) - 1)
                    Console.Write('+');
                else
                    Console.Write('-');
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// this function converts a board to a string
    /// </summary>
    /// <param name="board"></param>
    /// <returns>the board as a string</returns>
    public string BoardToString(Board board)
    {
        string boardElements = "";
        foreach (Cell cell in board.Cells)
        {
            boardElements += cell.Value;
        }
        return boardElements;
    }

    /// <summary>
    /// this function prints the solved sudoku board and returns the solve as a string
    /// </summary>
    /// <returns>the solve as a string
    /// </returns>
    public string Solve()
    {
        sudokuBoard.FindOptions();
        sudokuBoard.SolveBoard(0, 0);
        PrintBoard();
        return BoardToString(sudokuBoard);
    }
}