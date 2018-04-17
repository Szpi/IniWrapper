using System;

namespace IniWrapper.Handlers.NullValue
{
    public class NullValueHandler : IHandler
    {
        public object ParseReadValue(Type destinationType, string readValue)
        {
            return readValue;
        }

        public string FormatToWrite(object objectToFormat)
        {
            return string.Empty;
        }
    }
}