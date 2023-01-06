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
