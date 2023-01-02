using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

static class BoardValidations
{
    public static void ValidateDuplicatesInRows(Board board)
    {
        int rowElement;
        for (int i = 0; i < board.Side; i++)
        {
            for (int j = 0; j < board.Side; j++)
            {
                rowElement = board.Cells[i, j].Value;
                for (int k = 0; k < board.Side; k++)
                {
                    if (k != j && board.Cells[i, k].Value == rowElement)
                        throw new DuplicateElementsException($"An element can't appear more than once in a row!\n(element: {rowElement} in row: {i}");
                }
            }
        }
    }

    public static void ValidateDuplicatesInColumns(Board board)
    {
        int colElement;
        for (int i = 0; i < board.Side; i++)
        {
            for (int j = 0; j < board.Side; j++)
            {
                colElement = board.Cells[j, i].Value;
                for (int k = 0; k < board.Side; k++)
                {
                    if (k != j && board.Cells[k, i].Value == colElement)
                        throw new DuplicateElementsException($"An element can't appear more than once in a column!\n(element: {colElement} in column: {i}");
                }
            }
        }
    }

    public static void ValidateDuplicatesInBlocks(Board board)
    {
        Location blockIndex = new Location(0, 0);
        int blockElement;
        for (int i = 0; i < board.Side; i++)
        {
            blockIndex.Row = i / 3 % 3;
            blockIndex.Col = i % 3;
            for (int j = blockIndex.Row; j < blockIndex.Row + Math.Sqrt(board.Side); j++)
            {
                for (int k = blockIndex.Col; k < blockIndex.Col + Math.Sqrt(board.Side); k++)
                {
                    blockElement = board.Cells[j, k].Value;
                    for (int n = blockIndex.Col; n < blockIndex.Col + Math.Sqrt(board.Side); n++)
                    {
                        if(board.Cells[j, n].Value == blockElement)
                            throw new DuplicateElementsException($"An element can't appear more than once in a block!\n(element: {blockElement} in block: {i}");
                    }
                }
            }
        }
    }

    /// <summary>
    /// this function checks if we can assign a value in a row
    /// </summary>
    /// <param name="board"></param>
    /// <param name="row"></param>
    /// <param name="num"></param>
    /// <returns>true if the value can be assigned, false if not</returns>
    public static bool AssignableInRow(Board board, int row, int num)
    {
        for (int i = 0; i < board.Side; i++)
        {
            if (board.Cells[row, i].Value == num)
                return false;
        }
        return true;
    }

    /// <summary>
    /// this function checks if we can assign a value in a column
    /// </summary>
    /// <param name="board"></param>
    /// <param name="col"></param>
    /// <param name="num"></param>
    /// <returns>true if the value can be assigned, false if not</returns>
    public static bool AssignableInColumn(Board board, int col, int num)
    {
        for (int i = 0; i < board.Side; i++)
        {
            if (board.Cells[i, col].Value == num)
                return false;
        }
        return true;
    }

    /// <summary>
    /// this function checks if we can assign a value in a block (sub square)
    /// </summary>
    /// <param name="board"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <param name="num"></param>
    /// <returns>true if the value can be assigned, false if not</returns>
    public static bool AssignableInBlock(Board board, int row, int col, int num)
    {
        for (int i = row; i < row + Math.Sqrt(board.Side); i++)
        {
            for (int j = col; j < col + Math.Sqrt(board.Side); j++)
            {
                if (board.Cells[i, j].Value == num)
                    return false;
            }
        }
        return true;
    }

    /// <summary>
    /// this function checks if a value can be assigned into a Cell
    /// </summary>
    /// <param name="board"></param>
    /// <param name="cell"></param>
    /// <param name="num"></param>
    /// <returns>true if the value can be assigned, false if not</returns>
    public static bool IsAssignable(Board board, Cell cell, int num)
    {
        if (AssignableInRow(board, cell.Index.Row, num) && AssignableInColumn(board, cell.Index.Col, num) && AssignableInBlock(board, cell.BlockIndex.Row, cell.BlockIndex.Col, num))
            return true;
        return false;
    }
}
