//module for a calculation util
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// class for representing the grid
/// </summary>
public class Board
{
    private Cell[,] cells;
    private int side;
    private int minValue;
    private int maxValue;
    /// <summary>
    /// this function creates a new Board
    /// </summary>
    /// <param name="input"></param>
    public Board(string input)
    {
        //checking that the input is valid before starting
        try
        {
            InputValidations.PreCalculating(input);
        }
        catch(InputLengthException)
        {
            throw;
        }
        catch(ArgumentException)
        {
            throw;
        }
        //initialize the board
        side = (int)Math.Sqrt(input.Length);
        minValue = 1;
        maxValue = side;
        cells = new Cell[side, side];
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                //initializing the cells
                cells[i, j] = new Cell(new Location(i, j), input[i * side + j] - '0', side);
            }
        }
        BoardValidations.ValidateDuplicates(this);
    }

    /// <summary>
    /// this function finds the value options for the missing cells
    /// </summary>
    //public void FindOptions()
    //{
    //    for (int i = 0; i < side; i++)
    //    {
    //        for (int j = 0; j < side; j++)
    //        {
    //            cells[i, j].AddCandidates(this);
    //            if (cells[i, j].ValueOptions.Count == 1)
    //            {
    //                cells[i, j].Value = cells[i, j].ValueOptions.First();
    //            }
    //        }
    //    }
    //}

    //cells property
    public Cell[,] Cells
    {
        get { return cells; }
        set { cells = value; }
    }

    //side property
    public int Side
    {
        get { return side; }
        set { side = value; }
    }

    //minValue property
    public int MinValue
    {
        get { return minValue; }
        set { minValue = value; }
    }

    //maxValue property
    public int MaxValue
    {
        get { return maxValue; }
        set { maxValue = value; }
    }
}