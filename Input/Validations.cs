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
    private string input;
    private double side;
    public Validations(string input)
    {
        this.input = input;
        this.side = Math.Sqrt(input.Length);
    }

    private void ValidateInputLength()
    {
        for (int i = Constants.MIN_SIZE; i <= Constants.MAX_SIZE; i++)
        {
            if (side == Math.Pow(i, 2))
                return;
        }
        throw new InputLengthException("The input length is illegal!");
    }

    public HashSet<int> GetValidValues()
    {
        HashSet<int> validValues = new HashSet<int>();
        for (int i = 0; i < side; i++)
        {
            validValues.Add(i);
        }
        return validValues;
    }

    private void ValidateElementsValues()
    {
        HashSet<int> validValues = GetValidValues();
        foreach (char element in input)
        {
            if (!validValues.Contains(element - '0'))
                throw new ArgumentException(element + " is not a valid value in Omega Sudoku Board!");
        }
    }
    /// <summary>
    /// this function scans the given input and checks if it is valid before we are doing any calculations.
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="InputLengthException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void PreCalculating(string input)
    {
        //check if the input length is valid
        ValidateInputLength();
        // check if all the elements in the input are valid
        ValidateElementsValues();
    }

    /// <summary>
    /// this function checks if we can assign a value in a row
    /// </summary>
    /// <param name="board"></param>
    /// <param name="row"></param>
    /// <param name="num"></param>
    /// <returns>true if the value can be assigned, false if not</returns>
    public bool AssignableInRow(Board board, int row, int num)
    {
        for (int i = 0; i < side; i++)
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
    public bool AssignableInColumn(Board board, int col, int num)
    {
        for (int i = 0; i < side; i++)
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
    public bool AssignableInBlock(Board board, int row, int col, int num)
    {
        for (int i = row; i < row + Math.Sqrt(side); i++)
        {
            for (int j = col; j < col + Math.Sqrt(side); j++)
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
    public bool IsAssignable(Board board, Cell cell, int num)
    {
        if (AssignableInRow(board, cell.Index.Row, num) && AssignableInColumn(board, cell.Index.Col, num) && AssignableInBlock(board, cell.BlockIndex.Row, cell.BlockIndex.Col, num))
            return true;
        return false;
    }

    public double Side
    {
        get { return side; }
        set { this.side = value; }
    }
}
