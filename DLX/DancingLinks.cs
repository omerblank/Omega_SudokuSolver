using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

class DancingLinks
{
    private ColumnNode header;
    private List<DataNode> solution;

    public DancingLinks(CoverBoard coverMatrix)
    {
        solution = new List<DataNode>();
        header = MakeDLXBoard(coverMatrix);
    }

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
    private void PrintBoard()
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

    public bool Solve(int k)
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
            if (Solve(k + 1))
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

    public List<DataNode> Solution
    {
        get { return solution; }
        set { solution = value; }
    }
}
