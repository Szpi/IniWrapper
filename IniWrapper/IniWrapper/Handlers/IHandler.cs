using System;
using System.Collections.Generic;
using IniWrapper.Manager;

namespace IniWrapper.Handlers
{
    public interface IHandler
    {
        object ParseReadValue(Type destinationType, string readValue);
        IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue);
    }
}