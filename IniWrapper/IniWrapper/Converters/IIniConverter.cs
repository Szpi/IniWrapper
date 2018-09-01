using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters
{
    public interface IIniConverter
    {
        object ParseReadValue(string readValue, Type destinationType, IniContext iniContext);

        IniValue FormatToWrite(object objectToFormat, IniContext iniContext);
    }
}