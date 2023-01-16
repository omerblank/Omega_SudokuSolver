//module for a calculation util that belongs to DLX algorithm
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// /// class for representing a cover board
/// </summary>
class CoverBoard
{
    private int[,] coverMat; //a cover matrix (binary matrix)
    public const int ON = 1;//value of a bit that is ON
    public const int OFF = 0;//value of a bit that is OFF

    /// <summary>
    /// this function intializes the cell constraint in the cover matrix
    /// </summary>
    /// <param name="side"> the grid's side size </param>
    private void InitializeCellConstraint(int side)
    {
        for (int row = 0; row < coverMat.GetLength(0); row++)
        {
            //running on the first N^2 columns in the matrix
            for (int col = 0; col < Math.Pow(side, 2); col++)
            {
                if (row / side == col)
                {
                    coverMat[row, col] = ON;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// this function intializes the row constraint in the cover matrix
    /// </summary>
    /// <param name="side"> the grid's side size </param>
    private void InitializeRowConstraint(int side)
    {
        for (int row = 0; row < coverMat.GetLength(0); row++)
        {
            //running on the second N^2 columns in the matrix
            for (int col = (int)Math.Pow(side, 2); col < 2 * (int)Math.Pow(side, 2); col++)
            {
                if (row % (int)Math.Pow(side, 2) == col % (int)Math.Pow(side, 2))
                {
                    coverMat[row, col] = ON;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// this function intializes the column constraint in the cover matrix
    /// </summary>
    /// <param name="side"> the grid's side size </param>
    private void InitializeColConstraint(int side)
    {
        for (int row = 0; row < coverMat.GetLength(0); row++)
        {
            //running on the third N^2 columns in the matrix
            for (int col = (int)(2 * Math.Pow(side, 2)); col < 3 * Math.Pow(side, 2); col++)
            {
                int x = row % side + (row / (int)Math.Pow(side, 2)) * side;
                if (row % side + (row / (int)Math.Pow(side, 2)) * side == col % (int)Math.Pow(side, 2))
                {
                    coverMat[row, col] = ON;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// this function intializes the box constraint in the cover matrix
    /// </summary>
    /// <param name="side"> the grid's side size </param>
    private void InitializeBoxConstraint(int side)
    {
        Location boxIndex;
        int boxNumber, cellRow, cellCol;

        for (int row = 0; row < coverMat.GetLength(0); row++)
        {
            for (int col = (int)(3 * Math.Pow(side, 2)); col < 4 * Math.Pow(side, 2); col++)
            {
                cellRow = row / (int)Math.Pow(side, 2);
                cellCol = row % (int)Math.Pow(side, 2) / side;
                boxIndex = new Location(cellRow - cellRow % (int)Math.Sqrt(side), cellCol - cellCol % (int)Math.Sqrt(side));
                boxNumber = boxIndex.Row + boxIndex.Col / (int)Math.Sqrt(side);
                if (col % Math.Pow(side, 2) == row % Math.Pow(side, 2))
                {
                    coverMat[row, (int)(3 * Math.Pow(side, 2)) + boxNumber * side + col % side] = ON;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// this function turning OFF rows in the cover board according to the elements in the given grid
    /// </summary>
    /// <param name="board"> the grid </param>
    private void GridToCoverBoard(Board board)
    {
        int coverMatRow;
        for (int row = 0; row < board.Side; row++)
        {
            for (int col = 0; col < board.Side; col++)
            {
                for (int value = board.MinValue; value <= board.MaxValue; value++)
                {
                    //if there is a cell with value in the grid, turning OFF (put 0) in the right row in the cover matrix
                    if (board.Cells[row, col].Value != value && board.Cells[row, col].Value != 0)
                    {
                        coverMatRow = row * (int)Math.Pow(board.Side, 2) + col * board.Side + (value - 1);
                        for (int coverMatCol = 0; coverMatCol < coverMat.GetLength(1); coverMatCol++)
                        {
                            coverMat[coverMatRow, coverMatCol] = OFF;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// this function creates a cover board
    /// </summary>
    /// <param name="board"> the grid </param>
    public CoverBoard(Board board)
    {
        coverMat = new int[(int)Math.Pow(board.Side, 3), (int)Math.Pow(board.Side, 2) * 4];
        InitializeCellConstraint(board.Side);
        InitializeRowConstraint(board.Side);
        InitializeColConstraint(board.Side);
        InitializeBoxConstraint(board.Side);
        GridToCoverBoard(board);
    }

    //coverMat property
    public int[,] CoverMat
    {
        get { return coverMat; }
        set { coverMat = value; }
    }
}
