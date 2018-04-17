using System;

namespace IniWrapper.Handlers
{
    public interface IHandler
    {
        object ParseReadValue(Type destinationType, string readValue);
        string FormatToWrite(object objectToFormat);
    }
}