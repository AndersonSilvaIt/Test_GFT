using System;

namespace Console_GFT.Extensions
{
    public class ErrorMessageException : Exception
    {
        public ErrorMessageException(string message) : base(message) { }
    }
}