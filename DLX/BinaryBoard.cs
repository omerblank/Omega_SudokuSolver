using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
