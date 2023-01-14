﻿//module for running the program and showing results to the user
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Sudoku
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
            if(!dlxSolver.Search())
                throw new UnsolvableGridException("The grid is unsolvable!");
            dlxSolver.DlxToGrid(board);
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
    public static void SudokuRunner()
    {
        try
        {
            Messages.ChooseMode();
            Messages.MakeAChoice("c", "continue", SudokuRunner);
        }
        catch (InputLengthException ile)
        {
            Console.WriteLine(ile.Message);
            Messages.MakeAChoice("c", "continue", SudokuRunner);
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
            Messages.MakeAChoice("c", "continue", SudokuRunner);
        }
        catch(DuplicateElementsException dee)
        {
            Console.WriteLine(dee.Message);
            Messages.MakeAChoice("c", "continue", SudokuRunner);
        }
        //catch(UnsolvableGridException uge)
        //{
        //    Console.WriteLine(uge.Message);
        //    Messages.MakeAChoice("c", "continue", SolverRunning);
        //}
    }

    /// <summary>
    /// this function running the whole program
    /// </summary>
    public static void SudokuMain()
    {
        Messages.WelcomeMessasge();
        SudokuRunner();
        Messages.GoodbyeMessage();
    }
}