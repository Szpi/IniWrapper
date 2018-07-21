using System;
using System.Runtime.Serialization;

namespace IniWrapper.Exceptions
{
    [Serializable]
    public class CollectionOfComplexTypeException : Exception
    {
        public CollectionOfComplexTypeException() : base("Collection of complex type not supported")
        {
        }

        protected CollectionOfComplexTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}