using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Cell
{
    private Location index;
    private Location blockIndex;
    private int value;
    private List<int> candidates; // value options

    /// <summary>
    /// this function creates a new Cell
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public Cell(Location index, int value, int side)
    {
        this.index = index;
        this.value = value;
        blockIndex = new Location(index.Row - index.Row % (int)Math.Sqrt(side), index.Col - index.Col % (int)Math.Sqrt(side));
        candidates = new List<int>();
    }

    /// <summary>
    /// this function finds the missing values in the row
    /// </summary>
    /// <param name="board">the board</param>
    /// <returns>a counting array that represents the missing numbers in the column</returns>
    public int[] FindMissingInRow(Board board)
    {
        //counting array to check which numbers are missing
        int[] countArr = new int[board.Side];

        //initialize the counting array to 0
        Array.Clear(countArr, 0, countArr.Length);

        //loop in O(log n) to initizalize the counting array according to the given array
        for (int i = 0; i <= board.Side - i - 1; i++)
        {
            if (board.Cells[index.Row, i].value != 0)
                countArr[board.Cells[index.Row, i].value - 1]++;
            if (board.Cells[index.Row, board.Side - i - 1].value != 0)
                countArr[board.Cells[index.Row, board.Side - i - 1].value - 1]++;
        }
        return countArr;
    }

    /// <summary>
    /// this function finds the missing values in a column
    /// </summary>
    /// <param name="board">the board</param>
    /// <returns>a counting array that represents the missing numbers in the column</returns>
    public int[] FindMissingInColumn(Board board)
    {
        //counting array to check which numbers are missing
        int[] countArr = new int[board.Side];

        //initialize the counting array to 0
        Array.Clear(countArr, 0, countArr.Length);

        //loop in O(log n) to initizalize the counting array according to the given array
        for (int i = 0; i <= board.Side - i - 1; i++)
        {
            if (board.Cells[i, index.Col].value != 0)
                countArr[board.Cells[i, index.Col].value - 1]++;
            if (board.Cells[board.Side - i - 1, index.Col].value != 0)
                countArr[board.Cells[board.Side - i - 1, index.Col].value - 1]++;
        }
        return countArr;
    }

    /// <summary>
    /// this function finds the missing values in a sub square
    /// </summary>
    /// <param name="board">the board</param>
    /// <returns>a counting array that represents the missing numbers in the sub square</returns>
    public int[] FindMissingInSubSquare(Board board)
    {
        //counting array to check which numbers are missing
        int[] countArr = new int[board.Side];

        //initialize the counting array to 0
        Array.Clear(countArr, 0, countArr.Length);

        for (int i = blockIndex.Row; i < blockIndex.Row + Math.Sqrt(board.Side); i++)
        {
            for (int j = blockIndex.Col; j < blockIndex.Col + Math.Sqrt(board.Side); j++)
            {
                if (board.Cells[i, j].value != 0)
                    countArr[board.Cells[i, j].value - 1]++;
            }
        }
        return countArr;
    }

    /// <summary>
    /// the function adds the value options for each cell in the board
    /// </summary>
    /// <param name="board">the board</param>
    public void AddCandidates(Board board)
    {
        int[] missingsInRow = FindMissingInRow(board);
        int[] missingsInCol = FindMissingInColumn(board);
        int[] missingsInSubSquare = FindMissingInSubSquare(board);
        for (int i = 0; i < board.Side && value == 0; i++)
        {
            if (missingsInRow[i] == 0 && missingsInCol[i] == 0 && missingsInSubSquare[i] == 0)
                candidates.Add(i + 1);
        }
    }

    //index property
    public Location Index
    {
        get { return index; }
        set { index = value; }
    }

    //blockIndex property
    public Location BlockIndex
    {
        get { return blockIndex; }
        set { blockIndex = value; }
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
        get { return candidates; }
        set { candidates = value; }
    }

    //ToString (the value of the cell)
    public override string ToString()
    {
        return "" + value;
    }
}