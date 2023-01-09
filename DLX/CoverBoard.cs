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
            for (int col = 0; col < Math.Pow(side, side); col++)
            {
                if (row / side % side == col)
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
            for (int col = (int)Math.Pow(side, side); col < 2 * Math.Pow(side, side); col++)
            {
                if (row % side == col % side)
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
            for (int col = (int)(2 * Math.Pow(side, side)); col < 3 * Math.Pow(side, side); col++)
            {
                if (row == col % Math.Pow(side, side))
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
        for (int box = 0; box < side; box++)
        {
            for (int row = 0; row < coverMat.GetLength(0); row++)
            {
                for (int col = (int)(3 * Math.Pow(side, side)); col < 4 * Math.Pow(side, side); col++)
                {
                    if (row - row % (int)Math.Sqrt(side) == box && col - col % (int)Math.Sqrt(side) == box)
                    {
                        if (row % side == col % side)
                        {
                            coverMat[row, col] = ON;
                            break;
                        }
                    }
                }
            }
        }
    }

    public CoverBoard(int side)
    {
        coverMat = new int[(int)Math.Pow(side, 3), (int)Math.Pow(side, 2) * 4];
        InitializeCellConstraint(side);
        InitializeRowConstraint(side);
        InitializeColConstraint(side);
        InitializeBoxConstraint(side);
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
                    if (!board.Cells[row, col].ValueOptions.Contains(value))
                    {
                        coverMatRow = (int)(row * Math.Pow(board.Side, board.Side)) + col * board.Side;
                        for (int coverMatCol = 0; coverMatCol < coverMat.GetLength(1); coverMatCol++)
                        {
                            coverMat[coverMatRow, coverMatCol] = OFF;
                        }
                    }
                }
            }
        }
    }

    public int[,] CoverMat
    {
        get { return CoverMat; }
        set { CoverMat = value; }
    }
}
