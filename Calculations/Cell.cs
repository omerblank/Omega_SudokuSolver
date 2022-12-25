using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Cell
{
    private Location index;
    private int value;
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
        this.value = value;
        this.valueOptions = new List<int>();
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
        {
            if (board.Cells[index.Row, i].value != 0)
                countArr[board.Cells[index.Row, i].value - 1]++;
            if (board.Cells[index.Row, Constants.SIDE - i - 1].value != 0)
                countArr[board.Cells[index.Row, Constants.SIDE - i - 1].value - 1]++;
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
        {
            if (board.Cells[i, index.Col].value != 0)
                countArr[board.Cells[i, index.Col].value - 1]++;
            if (board.Cells[Constants.SIDE - i - 1, index.Col].value != 0)
                countArr[board.Cells[Constants.SIDE - i - 1, index.Col].value - 1]++;
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

        {
            {
                if (board.Cells[i, j].value != 0)
                    countArr[board.Cells[i, j].value - 1]++;
            }
        }
        return countArr;
    }

    public void AddValueOptions(Board board)
    {
        int[] missingsInRow = FindMissingInRow(board);
        int[] missingsInCol = FindMissingInColumn(board);
        int[] missingsInSubSquare = FindMissingInSubSquare(board);
        {
            if (missingsInRow[i] == 0 && missingsInCol[i] == 0 && missingsInSubSquare[i] == 0)
                valueOptions.Add(i + 1);
        }
    }

    //value property
    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    //valueOptions property
    public List<int> ValueOptions
    {
        get { return valueOptions; }
        set { valueOptions = value; }   
    }

    //ToString (the value of the cell)
    public override string ToString()
    {
        return "" + value;
    }
}