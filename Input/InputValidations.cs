//module for validations right when getting the input
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class InputValidations
{

    /// <summary>
    /// this function checks if the given grid as a string is in valid length
    /// </summary>
    /// <param name="input"> the given grid as a string </param>
    /// <exception cref="InputLengthException"> exception for invalid string length </exception>
    public static void ValidateInputLength(string input)
    {
        for (int i = Constants.MIN_SIZE; i <= Constants.MAX_SIZE; i++)
        {
            if (Math.Sqrt(input.Length) == Math.Pow(i, 2))
                return;
        }
        throw new InputLengthException("The input length is illegal!");
    }

    /// <summary>
    /// this function finds all possible values ​​in the board
    /// </summary>
    /// <param name="input"> the given grid as a string </param>
    /// <returns> all the possible values in the board as a hashset </returns>
    public static HashSet<int> GetValidValues(string input)
    {
        HashSet<int> validValues = new HashSet<int>();
        for (int i = 0; i <= Math.Sqrt(input.Length); i++)
        {
            validValues.Add(i);
        }
        return validValues;
    }

    /// <summary>
    /// this function checks if all the elements in the board have a valid value
    /// </summary>
    /// <param name="input"> the given grid as a string </param>
    /// <exception cref="ArgumentException"> exception for invalid value </exception>
    public static void ValidateElementsValues(string input)
    {
        HashSet<int> validValues = GetValidValues(input);
        foreach (char element in input)
        {
            if (!validValues.Contains(element - '0'))
                throw new ArgumentException(element + $" is not a valid value in {Math.Sqrt(input.Length)}X{Math.Sqrt(input.Length)} Omega Sudoku Board!");
        }
    }
    /// <summary>
    /// this function scans the given input and checks if it is valid before we are doing any calculations.
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="InputLengthException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static void PreCalculating(string input)
    {
        try
        {
            //check if the input length is valid
            ValidateInputLength(input);
            // check if all the elements in the input are valid
            ValidateElementsValues(input);
        }
        catch(InputLengthException)
        {
            throw;
        }
        catch(ArgumentException)
        {
            throw;
        }
    }
}
