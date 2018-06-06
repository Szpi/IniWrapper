using System;

namespace IniWrapper.Exceptions
{
    public class CollectionOfCopmexTypeException : Exception
    {
        public CollectionOfCopmexTypeException() : base("Collection of complex type not supported")
        {
        }
    }
}