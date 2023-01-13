//module for a calculation util that belongs to DLX algorithm
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// class for representing a column node
/// </summary>
class ColumnNode : DataNode
{
    private int size; //the size of the column node
    private string name; //the name of the column node

    /// <summary>
    /// this function creates a column node
    /// </summary>
    /// <param name="name"> the name of the column node </param>
    public ColumnNode(string name) : base()
    {
        size = 0;
        this.name = name;
        Column = this;
    }

    /// <summary>
    /// this function covers a column
    /// </summary>
    public void Cover()
    {
        HorizontalDisconnection();
        for (DataNode i = Down; i != this; i = i.Down)
        {
            for (DataNode j = i.Right; j != i; j = j.Right)
            {
                j.VerticalDisconnection();
                j.Column.size--;
            }
        }
    }

    /// <summary>
    /// this function uncovers a column
    /// </summary>
    public void Uncover()
    {
        for (DataNode i = Up; i != this; i = i.Up)
        {
            for (DataNode j = i.Left; j != i; j = j.Left)
            {
                j.VerticalReconnection();
                j.Column.size++;
            }
        }
        HorizontalReconnection();
    }

    //size property
    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    //name property
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}
