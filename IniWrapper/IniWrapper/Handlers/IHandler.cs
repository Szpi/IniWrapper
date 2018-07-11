using System;
using IniWrapper.Manager;

namespace IniWrapper.Handlers
{
    internal interface IHandler
    {
        object ParseReadValue(Type destinationType, string readValue);
        IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue);
    }
}