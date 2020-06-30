using System;

namespace MyProject.HandleException
{
    public class NotFoundExeception : Exception
    {
        public NotFoundExeception()
        {
        }

        public NotFoundExeception(string message) : base(message)
        {
        }
    }
}