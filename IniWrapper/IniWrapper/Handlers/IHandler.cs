using System;
using System.Collections.Generic;
using IniWrapper.Manager;

namespace IniWrapper.Handlers
{
    public interface IHandler
    {
        object ParseReadValue(Type destinationType, string readValue, IniValue iniValue);
        IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue);
    }
}