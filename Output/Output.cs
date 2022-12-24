using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Output
{
    private int side;//make this a constant
    private int[,] boardMat;

    /// <summary>
    /// this function builds a board object
    /// </summary>
    /// <param name="input"></param>
    public Output(string input)
    {
        this.side = (int)Math.Sqrt(input.Length);
        this.boardMat = new int[side, side];
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                this.boardMat[i, j] = input[i * this.side + j] - '0';
            }
        }
    }
    /// <summary>
    /// this function prints the game board at his current situation
    /// </summary>
    //todo: make the board look more nice!
    public void DrawBoard()
    {
        for (int i = 0; i < this.side; i++)
        {
            for (int j = 0; j < this.side; j++)
            {
                if (j < this.side - 1)
                    Console.Write(this.boardMat[i, j] + " | ");
                else
                    Console.Write(this.boardMat[i, j]);
            }
            Console.WriteLine();
            for (int k = 0; i < this.side - 1 && k < this.side * (Math.Sqrt(this.side) + 1) - Math.Sqrt(this.side); k++)
            {
                if ((k - Math.Sqrt(this.side) + 1) % ((int)Math.Sqrt(this.side) + 1) == 0 || (k + 1) == Math.Sqrt(this.side) && k < this.side * Math.Sqrt(this.side) - 1)
                    Console.Write('+');
                else
                    Console.Write('-');
            }
            Console.WriteLine();
        }
    }
}

