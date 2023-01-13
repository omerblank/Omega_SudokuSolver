using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

class CoverBoard
{
    private int[,] coverMat; //a cover matrix (binary matrix)
    public const int ON = 1;//value of a bit that is ON
    public const int OFF = 0;//value of a bit that is OFF
    /// <summary>
    /// this function intializes the cell constraint in the cover matrix
    /// </summary>
    /// <param name="side"> the grid's side size </param>
    public void InitializeCellConstraint(int side)
    {
        for (int row = 0; row < coverMat.GetLength(0); row++)
        {
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
    public void InitializeRowConstraint(int side)
    {
        for (int row = 0; row < coverMat.GetLength(0); row++)
        {
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
    public void InitializeColConstraint(int side)
    {
        for (int row = 0; row < coverMat.GetLength(0); row++)
        {
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
    public void InitializeBoxConstraint(int side)
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

    public void GridToCoverBoard(Board board)
    {
        int coverMatRow;
        for (int row = 0; row < board.Side; row++)
        {
            for (int col = 0; col < board.Side; col++)
            {
                for (int value = board.MinValue; value <= board.MaxValue; value++)
                {
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

    public CoverBoard(Board grid)
    {
        coverMat = new int[(int)Math.Pow(grid.Side, 3), (int)Math.Pow(grid.Side, 2) * 4];
        InitializeCellConstraint(grid.Side);
        InitializeRowConstraint(grid.Side);
        InitializeColConstraint(grid.Side);
        InitializeBoxConstraint(grid.Side);
        GridToCoverBoard(grid);
    }


    public int[,] CoverMat
    {
        get { return coverMat; }
        set { coverMat = value; }
    }
}
