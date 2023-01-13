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
    /// <returns> the given input as a string </returns>
    public static string ChooseMode()
    {
        string mode;
        Console.WriteLine($"Choose the way you want to give the input from the following options or type 'end' to end: ");
        Constants.INPUT_OPTIONS.ForEach(Console.WriteLine);
        mode = Console.ReadLine();
        while (!Constants.INPUT_OPTIONS.Contains(mode.ToLower()) && mode.ToLower() != "end")
        {
            Console.WriteLine($"Invalid input!\nchoose the way you want to give the input from the following options or type 'end' to end: ");
            Constants.INPUT_OPTIONS.ForEach(Console.WriteLine);
            mode = Console.ReadLine();
        }
        return OperateMode(mode);
    }

    /// <summary>
    /// this function select the mode according to the choice of the user
    /// </summary>
    /// <param name="mode"> the mode that the user chose </param>
    /// <returns> the given input as a string </returns>
    public static string OperateMode(string mode)
    {
        switch (mode.ToLower())
        {
            case "string":
                return StringInput();

            case "text file":
                return TextFileInput();

            case "end":
                {
                    GoodbyeMessage();
                    Environment.Exit(0);
                    break;
                }
        }
        return "";
    }

    /// <summary>
    /// this function gets a string from the user and returns it
    /// </summary>
    /// <returns> the given input as a string </returns>
    public static string StringInput()
    {
        string stringInput;
        Console.WriteLine("Enter the string: ");
        stringInput = Console.ReadLine();
        Console.WriteLine();
        return stringInput;
    }

    /// <summary>
    /// this function gets a text file from the user and returns it as a string
    /// </summary>
    /// <returns> the given input as a string </returns>
    public static string TextFileInput()
    {
        string textFileInput;
        Console.WriteLine("Enter the text file path: ");
        textFileInput = Console.ReadLine();
        try
        {
            textFileInput = File.ReadAllText(textFileInput);
            Console.WriteLine();
            return textFileInput;
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
        return "";
    }

    /// <summary>
    /// this function lets the user to make a choice (operate something or continue)
    /// </summary>
    /// <param name="validChoice"> the input that the user need to write to operate something </param>
    /// <param name="doWhat"> what will be operated </param>
    /// <param name="operation"> the operation </param>
    public static void MakeAChoice(string validChoice, string doWhat, Action operation)
    {
        string choice;
        Console.WriteLine(($"\nΩ PRESS '{validChoice}' TO {doWhat} OR ENTER TO SKIP Ω\n"));
        choice = Console.ReadLine();
        while (choice.ToLower() != validChoice && choice != "")
        {
            Console.WriteLine(($"Ω INVALID INPUT, PRESS '{validChoice}' TO {doWhat} OR ENTER TO SKIP Ω\n"));
            choice = Console.ReadLine();
        }
        if (choice.ToLower() == validChoice)
            operation();
        Console.WriteLine();
    }

    //this function prints the rules of Omega Sudoku Solver
    public static void ShowRules()
    {
        Console.WriteLine($"In Omega Sudoku Solver you can enter a Sudoku Grid as a string.\n" +
            $"You can enter the string in the Console *Or* enter a text file that contains the string.\n\n" +
            $"Legal size for a board is N*N where N is an integer from 1-25 and the square root of N is an integer as well.\n" +
            $"Legal elements are elements with a value from 1-N.\n" +
            $"Two elements with the same value will not appear in the same row, column or box\n" +
            $"Here is an example for a 9X9 sudoku grid as a string >>>\n{Constants.STRING_GRID_EXAMPLE}\n\n" +
            $"The grid will look like this:\n");
        Solution.PrintBoard(new Board(Constants.STRING_GRID_EXAMPLE));
    }

    //this function prints a welcome message
    public static void WelcomeMessasge()
    {
        Console.WriteLine($"Ω Hi user, welcome to Omega Sudoku Solver! Ω\n" +
            $"Here you can insert an unsolved sudoku board as a string or as a text file\n");
        MakeAChoice("rules", "see sudoku rules", ShowRules);
    }

    //this function prints a goodbye message
    public static void GoodbyeMessage()
    {
        Console.WriteLine("Ω SEE YOU SOON! Ω");
    }
}
