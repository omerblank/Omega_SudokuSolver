using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Solving
{
    /// <summary>
    /// this function prints the board
    /// </summary>
    public static void PrintBoard(Board board)
    {
        for (int i = 0; i < board.Side; i++)
        {
            for (int j = 0; j < board.Side; j++)
            {
                if (j < board.Side - 1)
                    Console.Write(board.Cells[i, j] + " | ");
                else
                    Console.Write(board.Cells[i, j]);
            }
            Console.WriteLine();
            for (int k = 0; i < board.Side - 1 && k < board.Side * (Math.Sqrt(board.Side) + 1) - Math.Sqrt(board.Side); k++)
            {
                if ((k - Math.Sqrt(board.Side) + 1) % ((int)Math.Sqrt(board.Side) + 1) == 0 || (k + 1) == Math.Sqrt(board.Side) && k < board.Side * Math.Sqrt(board.Side) - 1)
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
    public static string BoardToString(Board board)
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
    public static string Solve(Board board)
    {
        Console.WriteLine("Before solving: ");
        PrintBoard(board);
        board.FindOptions();
        board.SolveBoard(0, 0);
        Console.WriteLine("After solving: ");
        PrintBoard(board);
        return BoardToString(board);
    }
}