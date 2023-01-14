//module for exceptions
using System;

/// <summary>
/// exception for invalid input length
/// </summary>
class InputLengthException : Exception
{
    public InputLengthException(string message)
        : base(message) { }
}

/// <summary>
/// exception for invalid duplicates in the grid
/// </summary>
class DuplicateElementsException : Exception
{
    public DuplicateElementsException(string message)
        : base(message) { }
}

/// <summary>
/// exception for unsolvable grids
/// </summary>
class UnsolvableGridException : Exception
{
    public UnsolvableGridException(string message)
        : base(message) { }
}