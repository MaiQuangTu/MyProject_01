using System;

namespace MyProject.HandleException
{
    public class BadRequestException : Exception
    {
        
        public BadRequestException(string message) : base(message)
        {
        }
    }
}