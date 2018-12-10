using System;
using System.Runtime.Serialization;

namespace IniWrapper.Exceptions
{
    [Serializable]
    public class CollectionOfComplexTypeException : Exception
    {
        public CollectionOfComplexTypeException(string message) : base(message)
        {
        }

        protected CollectionOfComplexTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}