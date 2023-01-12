using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DataNode
{
    private DataNode left;
    private DataNode right;
    private DataNode up;
    private DataNode down;
    private ColumnNode column;
    private int rowPlacement;

    //CONSTRUCTORS

    public DataNode()
    {
        left = right = up = down = this;
    }

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

    public void HorizontalDisconnection()
    {
        left.right = right;
        right.left = left;
    }

    public void HorizontalReconnection()
    {
        left.right = right.left = this;
    }

    public void VerticalDisconnection()
    {
        up.down = down;
        down.up = up;
    }

    public void VerticalReconnection()
    {
        up.down = down.up = this;
    }
    public DataNode Left
    {
        get { return left; }
        set { left = value; }
    }
    public DataNode Right
    {
        get { return right; }
        set { right = value; }
    }
    public DataNode Up
    {
        get { return up; }
        set { up = value; }
    }
    public DataNode Down
    {
        get { return down; }
        set { down = value; }
    }
    public ColumnNode Column
    {
        get { return column; }
        set { column = value; }
    }
    public int RowPlacement
    {
        get { return rowPlacement; }
        set { rowPlacement = value; }
    }
}
