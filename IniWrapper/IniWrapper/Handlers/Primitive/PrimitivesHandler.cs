using System;

namespace IniWrapper.Handlers.Primitive
{
    public sealed class PrimitivesHandler : IHandler
    {
        public object ParseReadValue(Type destinationType, string readValue)
        {
            return Convert.ChangeType(readValue, destinationType);
        }

        public string FormatToWrite(object objectToFormat)
        {
            return objectToFormat.ToString();
        }
    }
}