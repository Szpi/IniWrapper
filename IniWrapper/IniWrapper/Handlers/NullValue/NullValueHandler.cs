using System;
using System.Collections.Generic;
using IniWrapper.Manager;

namespace IniWrapper.Handlers.NullValue
{
    internal class NullValueHandler : IHandler
    {
        public object ParseReadValue(Type destinationType, string readValue)
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