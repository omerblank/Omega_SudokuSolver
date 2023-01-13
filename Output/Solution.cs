using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Solution
{
    /// <summary>
    /// this function prints the board
    /// </summary>
    public static void PrintBoard(Board board)
    {
        for (int i = 0; i < board.Side; i++)
        {
            for (int j = 0; j < board.Side; j++)
            {
                //check if this is the last element of the box we add a line to sperate boxes from the sides
                if (j < board.Side - 1 && (j + 1) % Math.Sqrt(board.Side) == 0)
                    Console.Write((char)(board.Cells[i, j].Value + '0') + Constants.BOX_COL_SEPERATOR);
                else
                    Console.Write((char)(board.Cells[i, j].Value + '0') + Constants.ELEMENTS_SEPERATOR);
            }

            //going down a line for the next row of the grid
            Console.WriteLine();

            //loop for printing the lines that separate boxes
            for (int k = 0; i < board.Side - 1 && (i + 1) % Math.Sqrt(board.Side) == 0 && k < (2 * Math.Sqrt(board.Side) + 1) * (Math.Sqrt(board.Side) - 2) + 2 * Math.Sqrt(board.Side) * 2 + Math.Sqrt(board.Side) - 1; k++)
            {
                Console.Write(Constants.BOX_ROW_SEPERATOR);

                //check if this is the last character of the line we go down a line
                if (k == ((2 * Math.Sqrt(board.Side) + 1) * (Math.Sqrt(board.Side) - 2) + 2 * Math.Sqrt(board.Side) * 2 + Math.Sqrt(board.Side) - 1) - 1)
                    Console.WriteLine();
            }
        }
    }

    /// <summary>
    /// this function converts a board to a string
    /// </summary>
    /// <param name="board"></param>
    /// <returns>the board as a string</returns>
    public static string BoardToString(Board board)
    {
        string boardElements = "";
        foreach (Cell cell in board.Cells)
        {
            boardElements += (char)cell.Value + '0';
        }
        return boardElements;
    }

    public static void DlxToGrid(List<DataNode> solution, Board board)
    {
        foreach (DataNode node in solution)
        {
            DataNode rcNode = node;
            int min = int.Parse(rcNode.Column.Name);

            for (DataNode tmp = node.Right; tmp != node; tmp = tmp.Right)
            {
                int val = int.Parse(tmp.Column.Name);

                if (val < min)
                {
                    min = val;
                    rcNode = tmp;
                }
            }

            // we get line and column
            int ans1 = int.Parse(rcNode.Column.Name);
            int ans2 = int.Parse(rcNode.Right.Column.Name);
            int r = ans1 / board.Side;
            int c = ans1 % board.Side;
            // and the affected value
            int num = (ans2 % board.Side) + 1;
            // we affect that on the result grid
            board.Cells[r, c].Value = num;
        }
    }

    /// <summary>
    /// this function prints the solved sudoku board and returns the solve as a string
    /// </summary>
    /// <returns>the solve as a string
    /// </returns>
    public static string Solve(Board board)
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        Console.WriteLine("Before solving: ");
        PrintBoard(board);
        //board.FindOptions();
        CoverBoard coverMat = new CoverBoard(board);
        //-----------------------------------------------------
        //for (int i = 0; i < 4 * 4 * 4; i++)
        //{
        //    for (int j = 0; j < 4 * 4 * 4; j++)
        //    {
        //        Console.Write(coverMat.CoverMat[i, j]);
        //    }
        //    Console.WriteLine();
        //}
        //-----------------------------------------------------
        DancingLinks dlxSolver = new DancingLinks(coverMat);
        dlxSolver.Solve(0);
        DlxToGrid(dlxSolver.Solution, board);
        //board.SolveBoard(0, 0);
        Console.WriteLine("\nAfter solving: ");
        PrintBoard(board);
        watch.Stop();
        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");// right now the best execution time is 20 ms!
        return BoardToString(board);
    }


    public static void SolverRunning()
    {
        Solve(new Board(Messages.ChooseMode()));
        Messages.MakeAChoice("c", "continue", SolverRunning);
    }

    public static void SudokuMain()
    {
        Messages.WelcomeMessasge();
        SolverRunning();
        Messages.GoodbyeMessage();
    }
}