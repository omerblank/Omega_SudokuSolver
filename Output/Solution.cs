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
                if (j < board.Side - 1)
                    Console.Write(board.Cells[i, j] + " | ");
                else
                    Console.Write(board.Cells[i, j]);
            }
            Console.WriteLine();
            for (int k = 0; i < board.Side - 1 && k < board.Side * (Math.Sqrt(board.Side) + 1) - Math.Sqrt(board.Side); k++)
            {
                if ((k - Math.Sqrt(board.Side) + 1) % ((int)Math.Sqrt(board.Side) + 1) == 0 || (k + 1) == Math.Sqrt(board.Side) && k < board.Side * Math.Sqrt(board.Side) - 1)
                    Console.Write('+');
                else
                    Console.Write('-');
            }
            Console.WriteLine();
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
            boardElements += cell.Value;
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
        board.FindOptions();
        CoverBoard coverMat = new CoverBoard(board);
        //-----------------------------------------------------
        //for (int i = 0; i < 4*4*4; i++)
        //{
        //    for (int j = 0; j < 4*4*4; j++)
        //    {
        //        Console.Write(coverMat.CoverMat[i,j]);
        //    }
        //    Console.WriteLine();
        //}
        //-----------------------------------------------------
        DancingLinks dlxSolver = new DancingLinks(coverMat);
        dlxSolver.Solve(0);
        DlxToGrid(dlxSolver.Solution, board);
        //board.SolveBoard(0, 0);
        Console.WriteLine("After solving: ");
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