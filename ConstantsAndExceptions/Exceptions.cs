using System;
    class InputLengthException : Exception
    {
        public InputLengthException(string message)
            : base(message) { }
    }

    class DuplicateElementsException : Exception
    {
        public DuplicateElementsException(string message)
            : base(message) { }
    }