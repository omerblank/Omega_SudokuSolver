//module for running the program and showing results to the user
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
        Console.WriteLine();
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
        Console.WriteLine();
    }

    /// <summary>
    /// this function converts a board to a string
    /// </summary>
    /// <param name="board"> the board </param>
    /// <returns> the board as a string </returns>
    public static string BoardToString(Board board)
    {
        string boardElements = "";
        foreach (Cell cell in board.Cells)
        {
            boardElements += (char)(cell.Value + '0');
        }
        return boardElements;
    }

    /// <summary>
    /// this function converts dlx solution list to a grid
    /// </summary>
    /// <param name="solution"> the dlx solution list </param>
    /// <param name="board"> the grid </param>
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
    /// this function prints the grid before and after solving it, and returns the solution as a string
    /// </summary>
    /// <returns>the solve as a string
    /// </returns>
    public static string Solve(Board board)
    {
        try
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Console.WriteLine("Before solving: ");
            PrintBoard(board);
            CoverBoard coverMat = new CoverBoard(board);
            DancingLinks dlxSolver = new DancingLinks(coverMat);
            dlxSolver.Solve();
            DlxToGrid(dlxSolver.Solution, board);
            //board.SolveBoard(0, 0);
            Console.WriteLine("\nAfter solving: ");
            PrintBoard(board);
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            return BoardToString(board);
        }
        catch (InputLengthException)
        {
            throw;
        }
        catch (ArgumentException)
        {
            throw;
        }
        //board.FindOptions();

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

    }

    /// <summary>
    /// this function running the sudoku solver in a loop
    /// </summary>
    public static void SolverRunning()
    {
        try
        {
            Solve(new Board(Messages.ChooseMode()));
            Messages.MakeAChoice("c", "continue", SolverRunning);
        }
        catch (InputLengthException ile)
        {
            Console.WriteLine(ile.Message);
            Messages.MakeAChoice("c", "continue", SolverRunning);
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
            Messages.MakeAChoice("c", "continue", SolverRunning);
        }
        catch(DuplicateElementsException dee)
        {
            Console.WriteLine(dee.Message);
            Messages.MakeAChoice("c", "continue", SolverRunning);
        }
        //catch(UnsolvableGridException uge)
        //{
        //    Console.WriteLine(uge.Message);
        //    Messages.MakeAChoice("c", "continue", SolverRunning);
        //}
        //finally
        //{
        //    Messages.MakeAChoice("c", "continue", SolverRunning);
        //}
    }

    /// <summary>
    /// this function running the whole program
    /// </summary>
    public static void SudokuMain()
    {
        Messages.WelcomeMessasge();
        SolverRunning();
        Messages.GoodbyeMessage();
    }
}