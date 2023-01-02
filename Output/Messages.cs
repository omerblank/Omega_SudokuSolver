using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Messages
{
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

    public static string OperateMode(string mode)
    {
        switch (mode.ToLower())
        {
            case "string":
                return StringInput();

            case "text file":
                return TextFileInput();

            case "end":
                { }
                break;
        }
        return "";
    }

    public static string StringInput()
    {
        string stringInput;
        Console.WriteLine("Enter the string: ");
        stringInput = Console.ReadLine();
        Console.WriteLine();
        return stringInput;
    }

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

    public static void MakeAChoice(string validChoice, string doWhat, Action operation)
    {
        string choice;
        Console.WriteLine(($"Ω PRESS '{validChoice}' TO {doWhat} OR ENTER TO SKIP Ω\n"));
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

    public static void ShowRules()
    {

    }

    public static void WelcomeMessasge()
    {
        Console.WriteLine($"Ω Hi user, welcome to Omega Sudoku Solver! Ω\n" +
            $"Here you can insert an unsolved sudoku board as a string or as a text file, I will solve it and show you the solution\n");
        MakeAChoice("rules", "see sudoku rules", ShowRules);
    }

    public static void GoodbyeMessage()
    {
        Console.WriteLine("Ω SEE YOU SOON! Ω");
    }
}
