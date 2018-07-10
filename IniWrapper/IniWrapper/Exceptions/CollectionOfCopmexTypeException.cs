using System;
using System.Runtime.Serialization;

namespace IniWrapper.Exceptions
{
    [Serializable]
    public class CollectionOfCopmexTypeException : Exception
    {
        public CollectionOfCopmexTypeException() : base("Collection of complex type is not supported")
        {
        }

        protected CollectionOfCopmexTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}