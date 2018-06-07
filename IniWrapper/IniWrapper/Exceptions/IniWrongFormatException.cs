using System;
using System.Runtime.Serialization;

namespace IniWrapper.Exceptions
{
    [Serializable]
    public class IniWrongFormatException : Exception
    {
        public IniWrongFormatException(string message) : base(message)
        {
        }

        protected IniWrongFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}