using System;

namespace IniWrapper.PrimitivesParsers.NullValue
{
    public class NullValueParser : IParser
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