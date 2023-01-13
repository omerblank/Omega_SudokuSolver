//module for a calculation util
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// class for representing the index of an element in two dimensions
/// </summary>
class Location
{
    private int row;
    private int col;

    /// <summary>
    /// this function creates a new Location
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public Location(int row, int col)
    {
        this.row = row; this.col = col;
    }

    //row property
    public int Row
    {
        get { return row; }
        set { row = value; }
    }

    //col property
    public int Col
    {
        get { return col; }
        set { col = value; }
    }
}
