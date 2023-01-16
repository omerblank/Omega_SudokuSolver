//module for a calculation util that belongs to DLX algorithm, this is the main calculation module
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/// <summary>
/// class for representing a dlx structure
/// </summary>
class DancingLinks
{
    private ColumnNode header; //the header of the DLX structure
    private List<DataNode> solution; //the solution list

    /// <summary>
    /// this function creates a dancing links structure
    /// </summary>
    /// <param name="coverMatrix"> the cover matrix of the grid </param>
    public DancingLinks(CoverBoard coverMatrix)
    {
        solution = new List<DataNode>();
        header = MakeDLXBoard(coverMatrix);
    }

    /// <summary>
    /// this function selects the column with the minimum size
    /// </summary>
    /// <returns> the column with the minimum size </returns>
    private ColumnNode SelectMinColumnNode()
    {
        ColumnNode minCol = (ColumnNode)header.Right;
        ColumnNode currCol = (ColumnNode)minCol.Right;
        int minColSize = minCol.Size;
        while (currCol != header)
        {
            if (currCol.Size < minColSize)
            {
                minColSize = currCol.Size;
                minCol = currCol;
            }
            currCol = (ColumnNode)currCol.Right;
        }
        return minCol;
    }

    /// <summary>
    /// this function creates a dlx board using the cover matrix of the grid
    /// </summary>
    /// <param name="coverBoard"> the cover matrix object </param>
    /// <returns> the header node of the dlx board </returns>
    private ColumnNode MakeDLXBoard(CoverBoard coverBoard)
    {
        ColumnNode headerNode = new ColumnNode("HEADER");
        ArrayList columnNodes = new ArrayList();
        for (int i = 0; i < coverBoard.CoverMat.GetLength(1); i++)
        {
            ColumnNode n = new ColumnNode(i.ToString());
            columnNodes.Add(n);
            headerNode = (ColumnNode)headerNode.ConnectRight(n);
        }
        headerNode = headerNode.Right.Column;
        for (int i = 0; i < coverBoard.CoverMat.GetLength(0); i++)
        {
            DataNode prev = null;
            for (int j = 0; j < coverBoard.CoverMat.GetLength(1); j++)
            {
                if (coverBoard.CoverMat[i, j] == CoverBoard.ON)
                {
                    ColumnNode col = (ColumnNode)columnNodes[j];
                    DataNode newNode = new DataNode(col);
                    if (prev == null)
                        prev = newNode;
                    col.Up.ConnectDown(newNode);
                    prev = prev.ConnectRight(newNode);
                    col.Size++;
                }
            }
        }
        headerNode.Size = coverBoard.CoverMat.GetLength(1);
        return headerNode;
    }

    /// <summary>
    /// this function solves the grid (adding the correct data nodes to the solution list)
    /// </summary>
    /// <returns> true if the grid solved successfully, if not, returns false </returns>
    public bool Search()
    {
        // if there are no column nodes 
        if (header.Right == header)
            return true;
        ColumnNode selectedCol = SelectMinColumnNode();
        selectedCol.Cover();
        for (DataNode colData = selectedCol.Down; colData != selectedCol; colData = colData.Down)
        {
            solution.Add(colData);
            for (DataNode otherColData = colData.Right; otherColData != colData; otherColData = otherColData.Right)
                otherColData.Column.Cover();
            if (Search())
                return true;
            colData = solution.Last();
            solution.Remove(solution.Last());
            selectedCol = colData.Column;
            for (DataNode otherColData = colData.Left; otherColData != colData; otherColData = otherColData.Left)
                otherColData.Column.Uncover();
        }

        // the grid is unsolvable
        selectedCol.Uncover();
        return false;
    }

    /// <summary>
    /// this function converts dlx solution list to a grid
    /// </summary>
    /// <param name="board"> the grid </param>
    public void DlxToGrid(Board board)
    {
        int minColName, currColName, nextColName, gridRow, gridCol, cellValue;
        foreach (DataNode node in solution)
        {
            DataNode rcNode = node;
            minColName = int.Parse(rcNode.Column.Name);

            for (DataNode tmp = node.Right; tmp != node; tmp = tmp.Right)
            {
                currColName = int.Parse(tmp.Column.Name);

                if (currColName < minColName)
                {
                    minColName = currColName;
                    rcNode = tmp;
                }
            }

            // getting the row and the column of the found cell
            minColName = int.Parse(rcNode.Column.Name);
            nextColName = int.Parse(rcNode.Right.Column.Name);
            gridRow = minColName / board.Side;
            gridCol = minColName % board.Side;

            // getting the value of the found cell
            cellValue = (nextColName % board.Side) + 1;

            // placing the value in the right place in the grid
            board.Cells[gridRow, gridCol].Value = cellValue;
        }
    }

    //solution property
    public List<DataNode> Solution
    {
        get { return solution; }
        set { solution = value; }
    }
}
