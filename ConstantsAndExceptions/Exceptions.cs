//module for exceptions
using System;

/// <summary>
/// exception for invalid input length
/// </summary>
public class InputLengthException : Exception
{
    public InputLengthException(string message)
        : base(message) { }
}

/// <summary>
/// exception for invalid duplicates in the grid
/// </summary>
public class DuplicateElementsException : Exception
{
    public DuplicateElementsException(string message)
        : base(message) { }
}

/// <summary>
/// exception for unsolvable grids
/// </summary>
public class UnsolvableGridException : Exception
{
    public UnsolvableGridException(string message)
        : base(message) { }
}

/// <summary>
/// exception for files that are not supported in the system (not txt)
/// </summary>
public class FileNotSupportedException : Exception
{
    public FileNotSupportedException(string message)
        : base(message) { }
}