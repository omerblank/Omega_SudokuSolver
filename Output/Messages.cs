using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Messages
{
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
}
