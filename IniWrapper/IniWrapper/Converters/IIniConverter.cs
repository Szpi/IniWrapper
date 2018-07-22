using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters
{
    public interface IIniConverter
    {
        object ParseReadValue(Type destinationType, string readValue);
        IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue);
    }
}