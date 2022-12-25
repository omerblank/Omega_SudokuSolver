class Program
{
    static public void Main(String[] args)
    {
        Output check = new Output("100000027000304015500170683430962001900007256006810000040600030012043500058001000");
        check.PreCalculation();

        Board mat = new Board("100000027000304015500170683430962001900007256006810000040600030012043500058001000");
        mat.FindOptions();
        
        foreach (var option in mat.Cells[1,0].ValueOptions)
        {
            Console.WriteLine(option);
        }
    }
}

