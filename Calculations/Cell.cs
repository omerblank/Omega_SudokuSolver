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
    /// this function finds the missing values in the given line (row/column)
    /// </summary>
    public int[] FindMissingInLine(Cell[] row)
    {
        //counting array to check which numbers are missing
        int[] countArr = new int[Constants.SIDE];

        //initialize the counting array to 0
        Array.Clear(countArr, 0, countArr.Length);

        //loop in O(log n) to initizalize the counting array according to the given array
        for (int i = 0; i < Constants.SIDE - i - 1; i++)
        {
            if (row[i].valueOptions.Count != 0)
                countArr[row[i].valueOptions.First() - 1]++;
            if (row[Constants.SIDE - i - 1].valueOptions.Count != 0)
                countArr[row[Constants.SIDE - i - 1].valueOptions.First() - 1]++;
        }
        return countArr;
    }

    /// <summary>
    /// this function finds the missing values in the given sub square
    /// </summary>
    public int[] FindMissingInMat(Cell[,] subSquare)
    {
        //counting array to check which numbers are missing
        int[] countArr = new int[Constants.SIDE];

        //initialize the counting array to 0
        Array.Clear(countArr, 0, countArr.Length);

        for (int i = 0; i < Math.Sqrt(Constants.SIDE); i++)
        {
            for (int j = 0; j < Math.Sqrt(Constants.SIDE); j++)
            {
                if (subSquare[i, j].valueOptions.Count != 0)
                    countArr[subSquare[i, j].valueOptions.First() - 1]++;
            }
        }

        return countArr;
    }
}
