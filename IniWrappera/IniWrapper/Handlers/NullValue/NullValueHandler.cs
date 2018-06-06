using System;
using IniWrapper.Manager;

namespace IniWrapper.Handlers.NullValue
{
    public class NullValueHandler : IHandler
    {
        public object ParseReadValue(Type destinationType, string readValue, IniValue iniValue)
        {
            return readValue;
        }

        public string FormatToWrite(object objectToFormat)
        {
            return string.Empty;
        }
    }
}