class SudokuSolver
{
    static public void Main(String[] args)
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        Output check = new Output("100000027000304015500170683430962001900007256006810000040600030012043500058001000");
        check.PrintBoard();
        check.Solve();
        watch.Stop();
        //right now the execution time is around 30 ms!
        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
    }
}

