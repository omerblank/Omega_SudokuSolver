using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Location
{
    private int row;
    private int col;
    public Location(int row, int col)
    {
        this.row = row; this.col = col;
    }
    public int Row
    {
        get { return row; }
        set { row = value; }
    }
    public int Col
    {
        get { return col; }
        set { col = value; }
    }
}
