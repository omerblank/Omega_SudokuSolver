////module for a calculation util
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// class for representing an element in the grid (Cell)
/// </summary>
class Cell
{
    private Location index;
    private Location blockIndex;
    private int value;
    private List<int> candidates; // value options

    /// <summary>
    /// this function create a new Cell
    /// </summary>
    /// <param name="index"> the index in the grid </param>
    /// <param name="value"> the value </param>
    /// <param name="side"> the side size of the grid </param>
    public Cell(Location index, int value, int side)
    {
        this.index = index;
        this.value = value;
        blockIndex = new Location(index.Row - index.Row % (int)Math.Sqrt(side), index.Col - index.Col % (int)Math.Sqrt(side));
        candidates = new List<int>();
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