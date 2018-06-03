using System;

namespace IniWrapper.Exceptions
{
    public class IniWrongFormatException : Exception
    {
        public IniWrongFormatException(string message) : base(message)
        {
        }
    }
}