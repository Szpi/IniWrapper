using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.NullValue
{
    internal class NullValueReplaceConverter : IIniConverter
    {
        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            throw new NotImplementedException();
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            iniContext.IniValue.Value = string.Empty;

            return iniContext.IniValue;
        }
    }
}