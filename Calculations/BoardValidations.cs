//module for validations after getting the input
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// class for validations on the board
/// </summary>
static class BoardValidations
{
    /// <summary>
    /// this function check for duplicated elements in a row
    /// </summary>
    /// <param name="board"> the grid </param>
    /// <exception cref="DuplicateElementsException"> exception to throw </exception>
    public static void ValidateDuplicatesInRows(Board board)
    {
        int rowElement;
        for (int row = 0; row < board.Side; row++)
        {
            for (int col = 0; col < board.Side; col++)
            {
                if (board.Cells[row, col].Value != Constants.NO_VALUE)
                {
                    rowElement = board.Cells[row, col].Value;
                    for (int k = 0; k < board.Side; k++)
                    {
                        if (k != col && board.Cells[row, k].Value == rowElement)
                            throw new DuplicateElementsException($"An element can't appear more than once in a row!\n(element: {(char)(rowElement + '0')} in row: {row})");
                    }
                }
            }
        }
    }

    /// <summary>
    /// this function check for duplicated elements in a column
    /// </summary>
    /// <param name="board"> the grid </param>
    /// <exception cref="DuplicateElementsException"> exception to throw </exception>
    public static void ValidateDuplicatesInColumns(Board board)
    {
        int colElement;
        for (int col = 0; col < board.Side; col++)
        {
            for (int row = 0; row < board.Side; row++)
            {
                if (board.Cells[row, col].Value != Constants.NO_VALUE)
                {
                    colElement = board.Cells[row, col].Value;
                    for (int k = 0; k < board.Side; k++)
                    {
                        if (k != row && board.Cells[k, col].Value == colElement)
                            throw new DuplicateElementsException($"An element can't appear more than once in a column!\n(element: {(char)(colElement + '0')} in column: {col})");
                    }
                }
            }
        }
    }

    /// <summary>
    /// this function check for duplicated elements in a block
    /// </summary>
    /// <param name="board"> the grid </param>
    /// <exception cref="DuplicateElementsException"> exception to throw </exception>
    public static void ValidateDuplicatesInBlocks(Board board)
    {
        Location blockIndex = new Location(0, 0);
        int blockElement;
        for (int block = 0; block < board.Side; block++)
        {
            blockIndex.Row = block / (int)Math.Sqrt(board.Side) * (int)Math.Sqrt(board.Side);
            blockIndex.Col = block % (int)Math.Sqrt(board.Side) * (int)Math.Sqrt(board.Side);
            for (int row = blockIndex.Row; row < blockIndex.Row + Math.Sqrt(board.Side); row++)
            {
                for (int col = blockIndex.Col; col < blockIndex.Col + Math.Sqrt(board.Side); col++)
                {
                    if (board.Cells[row, col].Value != Constants.NO_VALUE)
                    {
                        blockElement = board.Cells[row, col].Value;
                        for (int blockRow = blockIndex.Row; blockRow < blockIndex.Row + Math.Sqrt(board.Side); blockRow++)
                        { 
                            for (int blockCol = blockIndex.Col; blockCol < blockIndex.Col + Math.Sqrt(board.Side); blockCol++)
                            {
                                if (!(blockRow == row && blockCol == col))
                                {
                                    if (board.Cells[blockRow, blockCol].Value == blockElement)
                                        throw new DuplicateElementsException($"An element can't appear more than once in a block!\n(element {(char)(blockElement + '0')} in block {block})");
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// this function check for illegal duplicates in the grid
    /// </summary>
    /// <param name="board"> the grid </param>
    public static void ValidateDuplicates(Board board)
    {
        ValidateDuplicatesInRows(board);
        ValidateDuplicatesInColumns(board);
        ValidateDuplicatesInBlocks(board);
    }
}
