using System;
using IniWrapper.Manager;

namespace IniWrapper.Handlers
{
    public interface IHandler
    {
        object ParseReadValue(Type destinationType, string readValue, IniValue iniValue);
        string FormatToWrite(object objectToFormat);
    }
}