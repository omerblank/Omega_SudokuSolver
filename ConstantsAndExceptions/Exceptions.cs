using System;
class InputLengthException : Exception
{
    public InputLengthException(string message)
        : base(message) { }
}