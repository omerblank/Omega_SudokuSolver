using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// class for validations on the input
static class InputValidations
{
    public static void ValidateInputLength(string input)
    {
        for (int i = Constants.MIN_SIZE; i <= Constants.MAX_SIZE; i++)
        {
            if (Math.Sqrt(input.Length) == Math.Pow(i, 2))
                return;
        }
        throw new InputLengthException("The input length is illegal!");
    }

    public static HashSet<int> GetValidValues(string input)
    {
        HashSet<int> validValues = new HashSet<int>();
        for (int i = 0; i <= Math.Sqrt(input.Length); i++)
        {
            validValues.Add(i);
        }
        return validValues;
    }

    public static void ValidateElementsValues(string input)
    {
        HashSet<int> validValues = GetValidValues(input);
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
    public static void PreCalculating(string input)
    {
        //check if the input length is valid
        ValidateInputLength(input);
        // check if all the elements in the input are valid
        ValidateElementsValues(input);
    }
}
