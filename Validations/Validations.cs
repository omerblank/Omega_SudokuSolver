using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// class for validations on the input
/// </summary>
class Validations
{
    /// <summary>
    /// this function scans the given input and checks if it is valid before we are doing any calculations.
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="InputLengthException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static void PreCalculating(string input)
    {
        //check if the input length is valid
        if (Math.Sqrt(input.Length) != Constants.SIDE)
            throw new InputLengthException("The input length does not fit the board");
        // check if all the elements in the input are valid
        foreach (char c in input)
        {
            if (c - '0' != 0 && !Constants.VALID_CELL_VALUES.Contains(c - '0'))
                throw new ArgumentException(c + " is not a valid value in Omega Sudoku Board!");
        }
    }

    /// <summary>
    /// this function checks if we can assign a value in a row
    /// </summary>
    /// <param name="board"></param>
    /// <param name="row"></param>
    /// <param name="num"></param>
    /// <returns>true if the value can be assigned, false if not</returns>
    public static bool AssignableInRow(Board board, int row, int num)
    {
        for (int i = 0; i < Constants.SIDE; i++)
        {
            if (board.Cells[row, i].Value == num)
                return false;
        }
        return true;
    }

    /// <summary>
    /// this function checks if we can assign a value in a column
    /// </summary>
    /// <param name="board"></param>
    /// <param name="col"></param>
    /// <param name="num"></param>
    /// <returns>true if the value can be assigned, false if not</returns>
    public static bool AssignableInColumn(Board board, int col, int num)
    {
        for (int i = 0; i < Constants.SIDE; i++)
        {
            if (board.Cells[i, col].Value == num)
                return false;
        }
        return true;
    }

    /// <summary>
    /// this function checks if we can assign a value in a block (sub square)
    /// </summary>
    /// <param name="board"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <param name="num"></param>
    /// <returns>true if the value can be assigned, false if not</returns>
    public static bool AssignableInBlock(Board board, int row, int col, int num)
    {
        for (int i = row; i < row + Math.Sqrt(Constants.SIDE); i++)
        {
            for (int j = col; j < col + Math.Sqrt(Constants.SIDE); j++)
            {
                if (board.Cells[i, j].Value == num)
                    return false;
            }
        }
        return true;
    }

    /// <summary>
    /// this function checks if a value can be assigned into a Cell
    /// </summary>
    /// <param name="board"></param>
    /// <param name="cell"></param>
    /// <param name="num"></param>
    /// <returns>true if the value can be assigned, false if not</returns>
    public static bool IsAssignable(Board board, Cell cell, int num)
    {
        if (AssignableInRow(board, cell.Index.Row, num) && AssignableInColumn(board, cell.Index.Col, num) && AssignableInBlock(board, cell.BlockIndex.Row, cell.BlockIndex.Col, num))
            return true;
        return false;
    }
}
