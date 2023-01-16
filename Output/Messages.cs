//module for printing messages for the user
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Messages
{

    /// <summary>
    /// this function let the user choose the way he wants to give the input
    /// </summary>
    public static void ChooseMode()
    {
        try
        {
            string mode;
            Console.WriteLine($"Choose the way you want to give the input from the following options or type \"end\" to end: \n");
            Constants.INPUT_OPTIONS.ForEach(Console.WriteLine);
            Console.WriteLine();
            mode = Console.ReadLine();
            while (!Constants.INPUT_OPTIONS.Contains(mode.ToLower()) && mode.ToLower() != "end")
            {
                Console.WriteLine($"\nΩ INVALID INPUT! Ω\nchoose the way you want to give the input from the following options or type \"end\" to end: \n");
                Constants.INPUT_OPTIONS.ForEach(Console.WriteLine);
                Console.WriteLine();
                mode = Console.ReadLine();
            }
            OperateMode(mode);
        }
        catch (NullReferenceException nre)
        {
            Console.WriteLine(nre.Message + "\n");
            ChooseMode();
        }
    }

    /// <summary>
    /// this function select the mode according to the choice of the user
    /// </summary>
    /// <param name="mode"> the mode that the user chose </param>
    public static void OperateMode(string mode)
    {
        switch (mode.ToLower())
        {
            case "string":
                StringInput();
                break;

            case "text file":
                TextFileInput();
                break;

            case "end":
                GoodbyeMessage();
                Environment.Exit(0);
                break;
        }
    }

    /// <summary>
    /// this function gets a string from the user and returns it
    /// </summary>
    public static void StringInput()
    {
        string stringGrid;
        Console.WriteLine("\nEnter the string: ");
        stringGrid = Console.ReadLine();
        Console.WriteLine();
        Sudoku.Solve(new Board(stringGrid));
    }

    /// <summary>
    /// this function gets a text file from the user and returns it as a string
    /// </summary>
    public static void TextFileInput()
    {
        string stringGrid, filePath;
        Console.WriteLine("\nEnter the text file path: ");
        filePath = Console.ReadLine();
        if (!filePath.EndsWith("txt"))
            throw new FileNotSupportedException("The system supports only text files!");
        try
        {
            stringGrid = File.ReadAllText(filePath);
            Console.WriteLine();
            File.WriteAllText(filePath, Sudoku.Solve(new Board(stringGrid)));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("the file was not found");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("the directory was not found");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("no permission to access the file");
        }
        catch (IOException)
        {
            Console.WriteLine("an I/O error occurred while reading the file");
        }
    }

    /// <summary>
    /// this function lets the user to make a choice (operate something or continue)
    /// </summary>
    /// <param name="validChoice"> the input that the user need to write to operate something </param>
    /// <param name="doWhat"> what will be operated </param>
    /// <param name="operation"> the operation </param>
    public static void MakeAChoice(string validChoice, string doWhat, Action operation)
    {
        try
        {
            string choice;
            Console.WriteLine(($"\nΩ PRESS '{validChoice}' TO {doWhat} OR Enter TO skip Ω\n"));
            choice = Console.ReadLine();
            while (choice.ToLower() != validChoice && choice != "")
            {
                Console.WriteLine(($"\nΩ INVALID INPUT, PRESS '{validChoice}' TO {doWhat} OR Enter TO skip Ω\n"));
                choice = Console.ReadLine();
            }
            Console.WriteLine();
            if (choice.ToLower() == validChoice)
                operation();
        }
        catch (NullReferenceException nre)
        {
            Console.WriteLine(nre.Message);
            MakeAChoice(validChoice, doWhat, operation);
        }
    }

    //this function prints the rules of Omega Sudoku Solver
    public static void ShowRules()
    {
        Console.WriteLine($"In Omega Sudoku Solver you can enter a Sudoku Grid as a string.\n" +
            $"You can enter the string in the Console *Or* enter a text file that contains ONLY the string.\n\n" +
            $"Legal size for a board is N*N where N is an integer from 1-25 and the square root of N is an integer as well.\n" +
            $"Legal elements are elements with a value from 1-N.\n" +
            $"0 symmbolizes an element with no value.\n" +
            $"the first N chars are in the first row.\n" +
            $"the second N chars are in the second row.\n" +
            $"Two elements with the same value will not appear in the same row, column or box.\n" +
            $"Here is an example for a 9X9 sudoku grid as a string >>>\n{Constants.STRING_GRID_EXAMPLE}\n\n" +
            $"The grid will look like this:\n");
        Sudoku.PrintBoard(new Board(Constants.STRING_GRID_EXAMPLE));
    }

    //this function prints a welcome message
    public static void WelcomeMessasge()
    {
        Console.WriteLine($"Ω HI USER, WELCOME TO OMEGA SUDOKU SOLVER! Ω\n" +
            $"Here you can insert a sudoku board as a string or as a text file");
        MakeAChoice("r", "see sudoku rules", ShowRules);
    }

    //this function prints a goodbye message
    public static void GoodbyeMessage()
    {
        Console.WriteLine("Ω SEE YOU SOON! Ω");
    }
}
