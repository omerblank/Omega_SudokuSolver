using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    public DataNode DownConnection(DataNode node)
    {
        node.down = this.down;
        node.down.up = node;
        node.up = this;
        this.down = node;
        return node;
    }

    /// <summary>
    /// this function connects a node to the right of "this" node
    /// </summary>
    /// <param name="node"> the node to connect </param>
    /// <returns> the connected node </returns>
    public DataNode RightConnection(DataNode node)
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

class ColumnNode : DataNode
{
    private int size;
    private char name;
    public ColumnNode(char name) : base()
    {
        this.size = 0;
        this.name = name;
        base.Column = this;
    }

    public void cover()
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

    public void uncover()
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


    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    public char Name
    {
        get { return name; }
        set { name = value; }
    }
}

class DancingLinks
{
    private ColumnNode header;
    private List<DataNode> answer;

    public DancingLinks(Board board)
    {
        header = makeDLXBoard(board);
    }

    private ColumnNode selectColumnNodeNaive()
    {
        return (ColumnNode)header.Right;
    }
    private void printBoard()
    { // diagnostics to have a look at the board state
        Console.WriteLine("The board: ");
        for (ColumnNode tmp = (ColumnNode)header.Right; tmp != header; tmp = (ColumnNode)tmp.Right)
        {

            for (DataNode d = tmp.Down; d != tmp; d = d.Down)
            {
                String solutionList = "";
                solutionList += d.Column.Name + ", ";
                for (DataNode i = d.Right; i != d; i = i.Right)
                {
                    solutionList += i.Column.Name + ", ";
                }
                Console.WriteLine(solutionList);
            }
        }
    }

    private ColumnNode makeDLXBoard(Board board)
    {
        ColumnNode headerNode = new ColumnNode('H');
        ArrayList columnNodes = new ArrayList();
        for (int i = 0; i < board.Side; i++)
        {
            ColumnNode n = new ColumnNode((char)i);
            columnNodes.Add(n);
            headerNode = (ColumnNode)headerNode.RightConnection(n);
        }
        headerNode = headerNode.Right.Column;
        for (int i = 0; i < board.Side; i++)
        {
            DataNode prev = null;
            for (int j = 0; j < board.Side; j++)
            {
                if (board.Cells[i, j].Value == 1)
                {
                    ColumnNode col = (ColumnNode)columnNodes[j];
                    DataNode newNode = new DataNode(col);
                    if (prev == null)
                        prev = newNode;
                    col.Up.DownConnection(newNode);
                    prev = prev.RightConnection(newNode);
                    col.Size++;
                }
            }
        }
        headerNode.Size = board.Side;
        return headerNode;
    }
}


class BinaryBoard
{
    private int[,] binaryMat;
    public BinaryBoard(Board board)
    {
        //indexers for the constraints
        int cellConstraint, rowConstraint, colConstraint, boxConstraint;
        //the binary matrix
        binaryMat = new int[(int)Math.Pow(board.Side, 3), (int)Math.Pow(board.Side, 2) * 4];
        for (int i = 0; i < binaryMat.GetLength(0); i++)
        {
            for (cellConstraint = 0; cellConstraint < (int)Math.Pow(board.Side, 2); cellConstraint++)
            {
                if (i / board.Side % board.Side == cellConstraint)
                {
                    binaryMat[i, cellConstraint] = 1;
                    break;
                }
            }
            for (rowConstraint = cellConstraint; rowConstraint < cellConstraint + (int)Math.Pow(board.Side, 2); rowConstraint++)
            {
                if (i % board.Side == rowConstraint % cellConstraint)
                {
                    binaryMat[i, rowConstraint] = 1;
                    break;
                }
            }
            for (colConstraint = rowConstraint; colConstraint < rowConstraint + (int)Math.Pow(board.Side, 2); colConstraint++)
            {
                if (i == colConstraint % rowConstraint)
                {
                    binaryMat[i, colConstraint] = 1;
                    break;
                }
            }
            for (boxConstraint = colConstraint; boxConstraint < colConstraint + (int)Math.Pow(board.Side, 2); boxConstraint++)
            {
                if (i / board.Side % board.Side == boxConstraint % colConstraint)
                {
                    binaryMat[i, boxConstraint] = 1;
                    break;
                }
            }
        }
    }
}
