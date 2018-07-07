using System;
using System.Collections.Generic;
using IniWrapper.Manager;

namespace IniWrapper.Handlers.NullValue
{
    public class NullValueHandler : IHandler
    {
        public object ParseReadValue(Type destinationType, string readValue, IniValue iniValue)
        {
            return readValue;
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            defaultIniValue.Value = string.Empty;

            return defaultIniValue;
        }
    }
}