using System;

namespace IniWrapper.PrimitivesParsers
{
    public interface IParser
    {
        object ParseReadValue(Type destinationType, string readValue);
        string FormatToWrite(object objectToFormat);
    }
}