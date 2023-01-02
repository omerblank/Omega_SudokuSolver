﻿using System;
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
    private int side;
    /// <summary>
    /// this function creates a new Board
    /// </summary>
    /// <param name="input"></param>
    public Board(string input)
    {
        //checking that the input is valid before starting
        Validations.PreCalculating(input);
        //initialize the board
        side = (int)Math.Sqrt(input.Length);
        cells = new Cell[side, side];
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                //initializing the cells
                cells[i, j] = new Cell(new Location(i, j), input[i * side + j] - '0', side);
            }
        }
    }

    /// <summary>
    /// this function finds the value options for the missing cells
    /// </summary>
    public void FindOptions()
    {
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                cells[i, j].AddValueOptions(this);
                if (cells[i, j].ValueOptions.Count == 1)
                {
                    cells[i, j].Value = cells[i, j].ValueOptions.First();
                }
            }
        }
    }

    public bool SolveBoard(int row, int col)
    {
        if (row == side - 1 && col == side)
            return true;

        if (col == side)
        {
            row++;
            col = 0;
        }

        if (cells[row, col].Value != 0)
            return SolveBoard(row, col + 1);

        foreach (int value in cells[row, col].ValueOptions)
        {
            if (Validations.IsAssignable(this, cells[row, col], value))
            {
                cells[row, col].Value = value;

                if (SolveBoard(row, col + 1))
                    return true;
            }
            cells[row, col].Value = 0;
        }
        return false;
    }
    public Cell[,] Cells
    {
        get { return cells; }
        set { cells = value; }
    }

    public int Side
    {
        get { return side; }
        set { side = value; }
    }
}