using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DancingLinks
{
    private ColumnNode header;
    private List<DataNode> answer;

    public DancingLinks(CoverBoard coverMatrix)
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

    private ColumnNode makeDLXBoard(CoverBoard coverBoard)
    {
        ColumnNode headerNode = new ColumnNode("HEADER");
        ArrayList columnNodes = new ArrayList();
        for (int i = 0; i < coverBoard.CoverMat.GetLength(1); i++)
        {
            ColumnNode n = new ColumnNode(i.ToString());
            columnNodes.Add(n);
            headerNode = (ColumnNode)headerNode.RightConnection(n);
        }
        headerNode = headerNode.Right.Column;
        for (int i = 0; i < coverBoard.CoverMat.GetLength(0); i++)
        {
            DataNode prev = null;
            for (int j = 0; j < coverBoard.CoverMat.GetLength(1); j++)
            {
                if (coverBoard.CoverMat[i,j] == CoverBoard.ON)
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
        headerNode.Size = coverBoard.Side;
        return headerNode;
    }
}
