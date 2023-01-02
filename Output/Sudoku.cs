using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Sudoku
{
    public static void SolveSudoku()
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        Messages.WelcomeMessasge();
        Solving.Solve(new Board(Messages.ChooseMode()));
        watch.Stop();
        //right now the best execution time is 20 ms!
        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
    }
}
