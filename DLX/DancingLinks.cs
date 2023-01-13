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
    public bool Solve()
    {
        if (header.Right == header)
            return true;
        ColumnNode selectedCol = SelectMinColumnNode();
        selectedCol.Cover();
        for (DataNode colData = selectedCol.Down; colData != selectedCol; colData = colData.Down)
        {
            solution.Add(colData);
            for (DataNode otherColData = colData.Right; otherColData != colData; otherColData = otherColData.Right)
                otherColData.Column.Cover();
            if (Solve())
                return true;
            colData = solution[solution.Count - 1];
            solution.Remove(solution[solution.Count - 1]);
            selectedCol = colData.Column;
            for (DataNode otherColData = colData.Left; otherColData != colData; otherColData = otherColData.Left)
                otherColData.Column.Uncover();
        }
        selectedCol.Uncover();
        return false;
    }

    //solution property
    public List<DataNode> Solution
    {
        get { return solution; }
        set { solution = value; }
    }
}
