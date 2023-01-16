//module for a calculation util that belongs to DLX algorithm
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// class for representing a data node
/// </summary>
class DataNode
{
    private DataNode left; // the data node from left
    private DataNode right; // the data node from right
    private DataNode up; // the data node from above
    private DataNode down; // the data node from below
    private ColumnNode column; // the column node
    private int rowPlacement; // the row placement

    /// <summary>
    /// this function creates a data node (adapted for a column node)
    /// </summary>
    public DataNode()
    {
        left = right = up = down = this;
    }

    /// <summary>
    /// this function creates a data node
    /// </summary>
    /// <param name="col"> the column node of the data node</param>
    public DataNode(ColumnNode col) : this()
    {
        column = col;
    }

    /// <summary>
    /// this function connects a node below "this" node
    /// </summary>
    /// <param name="node"> the node to connect </param>
    /// <returns> the connected node </returns>
    public DataNode ConnectDown(DataNode node)
    {
        node.down = down;
        node.down.up = node;
        node.up = this;
        down = node;
        return node;
    }

    /// <summary>
    /// this function connects a node to the right of "this" node
    /// </summary>
    /// <param name="node"> the node to connect </param>
    /// <returns> the connected node </returns>
    public DataNode ConnectRight(DataNode node)
    {
        node.right = right;
        node.right.left = node;
        node.left = this;
        right = node;
        return node;
    }

    /// <summary>
    /// this function disconnects a data node from the nodes in its sides
    /// </summary>
    public void HorizontalDisconnection()
    {
        left.right = right;
        right.left = left;
    }

    /// <summary>
    /// this function reconnects a data node to the nodes in its sides
    /// </summary>
    public void HorizontalReconnection()
    {
        left.right = right.left = this;
    }

    /// <summary>
    /// this function disconnects a data node from the nodes above and below it
    /// </summary>
    public void VerticalDisconnection()
    {
        up.down = down;
        down.up = up;
    }

    /// <summary>
    /// this function reconnects a data node to the nodes above and below it
    /// </summary>
    public void VerticalReconnection()
    {
        up.down = down.up = this;
    }

    //left property
    public DataNode Left
    {
        get { return left; }
        set { left = value; }
    }

    //right property
    public DataNode Right
    {
        get { return right; }
        set { right = value; }
    }

    //up property
    public DataNode Up
    {
        get { return up; }
        set { up = value; }
    }

    //down property
    public DataNode Down
    {
        get { return down; }
        set { down = value; }
    }

    //column property
    public ColumnNode Column
    {
        get { return column; }
        set { column = value; }
    }

    //rowPlacement property
    public int RowPlacement
    {
        get { return rowPlacement; }
        set { rowPlacement = value; }
    }
}
