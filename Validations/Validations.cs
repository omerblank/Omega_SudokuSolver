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
        if (Math.Sqrt(input.Length) != Constants.SIDE)
            throw new InputLengthException("The input length does not fit the board");
        foreach (char c in input)
        {
            if (c - '0' != 0 && !Constants.VALID_CELL_VALUES.Contains(c - '0'))
                throw new ArgumentException(c + " is not a valid value in Omega Sudoku Board!");
        }
    }
}
