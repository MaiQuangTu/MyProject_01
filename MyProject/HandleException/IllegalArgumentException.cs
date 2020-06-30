using System;

namespace MyProject.HandleException
{
    public class IllegalArgumentException : Exception
    {
        public IllegalArgumentException(string message) : base(message)
        {
        }
    }
}