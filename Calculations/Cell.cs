using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Cell
{
    private Location index;
    private Location subSquareTopLeftIndex;
    private List<int> valueOptions;

    /// <summary>
    /// this function initializes a Cell Object
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public Cell(Location index, int value)
    {
        int subSquareRow = -1;
        int subSquareCol = -1;
        this.index = index;
        for (int i = 0; i < Constants.SIDE; i++)
        {
            for (int j = 0; j < Math.Sqrt(Constants.SIDE); j++)
            {
                if (index.Row - j == i)
                    subSquareRow = i;
                if (index.Col - j == i)
                    subSquareCol = i;
            }
            if (subSquareRow != -1 && subSquareCol != -1)
                break;
        }
        this.subSquareTopLeftIndex = new Location(subSquareRow, subSquareCol);
        this.valueOptions = new List<int>();
        if (value != 0)
            this.valueOptions.Add(value);
    }

    /// <summary>
    /// this function finds the missing values in the row
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public int[] FindMissingInRow(Board board)
    {
        //counting array to check which numbers are missing
        int[] countArr = new int[Constants.SIDE];

        //initialize the counting array to 0
        Array.Clear(countArr, 0, countArr.Length);

        //loop in O(log n) to initizalize the counting array according to the given array
        for (int i = 0; i < Constants.SIDE - i - 1; i++)
        {
            if (board.Cells[index.Row, i].valueOptions.Count != 0)
                countArr[board.Cells[index.Row, i].valueOptions.First() - 1]++;
            if (board.Cells[index.Row, Constants.SIDE - i - 1].valueOptions.Count != 0)
                countArr[board.Cells[index.Row, Constants.SIDE - i - 1].valueOptions.First() - 1]++;
        }
        return countArr;
    }

    /// <summary>
    /// this function finds the missing values in the column
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public int[] FindMissingInColumn(Board board)
    {
        //counting array to check which numbers are missing
        int[] countArr = new int[Constants.SIDE];

        //initialize the counting array to 0
        Array.Clear(countArr, 0, countArr.Length);

        //loop in O(log n) to initizalize the counting array according to the given array
        for (int i = 0; i < Constants.SIDE - i - 1; i++)
        {
            if (board.Cells[i, index.Col].valueOptions.Count != 0)
                countArr[board.Cells[i, index.Col].valueOptions.First() - 1]++;
            if (board.Cells[Constants.SIDE - i - 1, index.Col].valueOptions.Count != 0)
                countArr[board.Cells[Constants.SIDE - i - 1, index.Col].valueOptions.First() - 1]++;
        }
        return countArr;
    }

    /// <summary>
    /// this function finds the missing values in the sub square
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public int[] FindMissingInSubSquare(Board board)
    {
        //counting array to check which numbers are missing
        int[] countArr = new int[Constants.SIDE];

        //initialize the counting array to 0
        Array.Clear(countArr, 0, countArr.Length);

        for (int i = subSquareTopLeftIndex.Row; i < Math.Sqrt(Constants.SIDE); i++)
        {
            for (int j = subSquareTopLeftIndex.Col; j < Math.Sqrt(Constants.SIDE); j++)
            {
                if (board.Cells[i, j].valueOptions.Count != 0)
                    countArr[board.Cells[i, j].valueOptions.First() - 1]++;
            }
        }
        return countArr;
    }

    //valueOptions property
    public List<int> ValueOptions
    {
        get { return valueOptions; }
    }

    //ToString (the value of the cell)
    public override string ToString()
    {
        return "" + valueOptions.First();
    }
}