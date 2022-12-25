using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// class for calculations on the board
/// </summary>
class Board
{
    private Cell[,] cells;

    /// <summary>
    /// this function initializes the calculations board.
    /// </summary>
    /// <param name="input"></param>
    public Board(string input)
    {
        //checking that the input is valid before starting
        Validations.PreCalculating(input);
        //initialize the board
        this.cells = new Cell[Constants.SIDE, Constants.SIDE];
        for (int i = 0; i < Constants.SIDE; i++)
        {
            for (int j = 0; j < Constants.SIDE; j++)
            {
                //initializing the cells
                this.cells[i, j] = new Cell(new Location(i, j), input[i * Constants.SIDE + j] - '0');
            }
        }
    }

    /// <summary>
    /// this function finds the value options for the missing cells
    /// </summary>
    public void FindOptions()
    {
        for (int i = 0; i < Constants.SIDE; i++)
        {
            for (int j = 0; j < Constants.SIDE; j++)
            {
                cells[i, j].AddValueOptions(this);
                if (cells[i, j].ValueOptions.Count == 1)
                {
                    cells[i, j].Value = cells[i, j].ValueOptions.First();
                }
            }
        }
    }
    public Cell[,] Cells
    {
        get { return cells; }
        set { cells = value; }
    }
}